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

    public Token NextToken()
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

    private ReadOnlySpan<char> PeekTakeWhile(Func<char, bool> predicate)
    {
        var start = position;

        while (NotTheEnd() && predicate(sql[position]))
        {
            position++;
        }

        return sql.Slice(start, position - start);
    }

    private readonly bool NotTheEnd() => position < sql.Length;

    private static readonly Dictionary<string, Keyword> _keywords = new(StringComparer.OrdinalIgnoreCase)
    {
        ["select"] = Keyword.Select,
        ["insert"] = Keyword.Insert,
        ["update"] = Keyword.Update,
        ["delete"] = Keyword.Delete,
        ["create"] = Keyword.Create,
        ["drop"] = Keyword.Drop,
        ["alter"] = Keyword.Alter,
        ["table"] = Keyword.Table,
        ["column"] = Keyword.Column,
        ["index"] = Keyword.Index,
        ["view"] = Keyword.View,
        ["join"] = Keyword.Join,
        ["inner join"] = Keyword.InnerJoin,
        ["left join"] = Keyword.LeftJoin,
        ["right join"] = Keyword.RightJoin,
        ["full join"] = Keyword.FullJoin,
        ["cross join"] = Keyword.CrossJoin
    };
    private readonly Func<char, bool> IdentifierPredicate = c => char.IsLetterOrDigit(c) || c is '$' or '_';

    private Token ReadIdentifierOrKeyword()
    {
        var slice = PeekTakeWhile(IdentifierPredicate);
        var identifier = slice.ToString();

        if (_keywords.TryGetValue(identifier, out var tokenType))
        {
            return new KeywordToken(identifier, tokenType);
        }

        return new Token(identifier, TokenType.Identifier);
    }

}