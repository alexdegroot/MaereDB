using MaereDb.Engine.QueryParser.Tokens;

namespace MaereDb.Engine.QueryParser;

internal ref struct Tokenizer
{
    private int position;
    private ReadOnlySpan<char> sql;

    public Tokenizer(ReadOnlySpan<char> sql)
    {
        this.sql = sql;
    }

    public IList<Token> Tokenize()
    {
        var tokens = new List<Token>(sql.Length / 4);

        while (true)
        {
            var token = NextToken();
            if (token.Type == TokenType.EndOfInput)
                break;
            tokens.Add(token);
        }

        return tokens;
    }

    private Token NextToken()
    {
        if (position >= sql.Length)
            return new Token(string.Empty, TokenType.EndOfInput);

        var character = sql[position];
        return character switch
        {
            ' ' => CreateToken(position, 1, TokenType.Whitespace),
            '+' or '-' or '*' or '/' or '%' => CreateToken(position, 1, TokenType.Operator),
            ',' => CreateToken(position, 1, TokenType.Operator),
            '=' => CreateToken(position, 1, TokenType.Operator),
            _ when char.IsDigit(character) => ReadDigit(),
            _ when IdentifierPredicate(character) => ReadIdentifierOrKeyword(),
            _ => throw new NotSupportedException($"Unexpected character '{character}' at position {position}.")
        };
    }

    private Token CreateToken(ReadOnlySpan<char> value, TokenType tokenType)
    {
        position++;
        return new Token(value.ToString(), tokenType);
    }

    private Token CreateToken(int start, int length, TokenType tokenType)
    {
        position += length;
        return new Token(sql.Slice(start, length).ToString(), tokenType);
    }

    private Token ReadDigit()
    {
        var start = position;

        while (NotTheEnd() && char.IsDigit(sql[position]))
        {
            position++;
        }

        return new Token(sql.Slice(start, position - start).ToString(), TokenType.Number);
    }

    internal ReadOnlySpan<char> PeekTakeWhile(Func<char, bool> predicate)
    {
        var start = position;

        while (NotTheEnd() && predicate(sql[position]))
        {
            position++;
        }

        return sql.Slice(start, position - start);
    }

    internal readonly bool NotTheEnd() => position < sql.Length;

    private static readonly Dictionary<string, TokenType> _keywords = new(StringComparer.OrdinalIgnoreCase)
    {
        ["select"] = TokenType.Keyword,
        ["from"] = TokenType.Keyword,
        ["where"] = TokenType.Keyword,
        ["as"] = TokenType.Keyword
    };
    private readonly Func<char, bool> IdentifierPredicate = c => char.IsLetterOrDigit(c) || c is '$' or '_';

    private Token ReadIdentifierOrKeyword()
    {
        var slice = PeekTakeWhile(IdentifierPredicate);
        var identifier = slice.ToString();

        if (_keywords.TryGetValue(identifier, out var tokenType))
        {
            return new Token(identifier, tokenType);
        }

        return new Token(identifier, TokenType.Identifier);
    }

}