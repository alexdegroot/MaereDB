using MaereDb.Engine.QueryParser.Tokens;

namespace MaereDb.Engine.QueryParser.Tests;

public class TokenizeSelectQueryTests
{
    [Fact]
    public void ParseExpressionField()
    {
        var sql = "SELECT 1+1 as SUM, 2+2 as SUM2 FROM T1 WHERE 1=1";
        var tokenizer = new Tokenizer(sql.AsSpan());

        var tokens = tokenizer.Tokenize();

        Assert.NotNull(tokens);
        Assert.Equal(28, tokens.Count);
        Assert.Equal(TokenType.Keyword, tokens.Single(t => t.Value == "WHERE").Type);
    }

    [Fact]
    public void ThrowExceptionOnIllegalCharacter()
    {
        var sql = "SELECT $^$%^%^%";
        

        Assert.Throws<NotSupportedException>(() =>
        {
            var tokenizer = new Tokenizer(sql.AsSpan());
            var tokens = tokenizer.Tokenize();
        });
    }

}