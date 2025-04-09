namespace MaereDb.Tests.Examples;

public class MathOperations
{
    public MathOperations(IExecutor executor)
    {
        _executor = executor;
    }
    private readonly IExecutor _executor;

    public int Add(int a, int b)
    {
        var sql = $"SELECT {a} + {b}";
        var result = _executor.ExecuteScalar(sql);
        return Convert.ToInt32(result);
    }
}