using MaereDb.Engine.QueryParser.Tokens;

namespace MaereDb.Engine.QueryParser;

internal ref struct Parser
{
    private readonly Tokenizer tokenizer;

    public Parser(Tokenizer tokenizer)
    {
        this.tokenizer = tokenizer;
    }

    public ParseResult Parse()
    {
        var token = tokenizer.NextToken();

        if(token is KeywordToken keywordToken)
        {
            return keywordToken.Keyword switch
            {
                Keyword.Select => ParseSelect(),
                _ => ParseResult.CreateFailure($"Keyword '{keywordToken.Keyword}' is incorrect or currently not supported."),
            };
        }
        return ParseResult.CreateFailure("Every query should start with a keyword");
    }

    private ParseResult ParseSelect()
    {
        throw new NotImplementedException();
    }
}