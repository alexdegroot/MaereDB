namespace MaereDb.Engine.QueryParser.Tests;

public class ParserSelectQueryTests
{
    [Fact]
    public void ParseExpressionField()
    {
        var sql = "SELECT 1+1 as SUM, 2+2 as SUM2 FROM T1 WHERE 1=1";
        var tokenizer = new Tokenizer(sql.AsSpan());
        var parser = new Parser(tokenizer);

        var result = parser.Parse();

        Assert.True(result.Success);
        Assert.NotNull(result.Statements);
    }
}
