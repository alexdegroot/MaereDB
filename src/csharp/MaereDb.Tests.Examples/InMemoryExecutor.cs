namespace MaereDb.Tests.Examples;

public class InMemoryDbEngine : IExecutor
{
    public object ExecuteScalar(string sql)
    {
        if(sql == "SELECT 2 + 2")
        {
            return 4;
        }
        else
        {
            throw new NotSupportedException($"SQL statement not supported: {sql}");
        }
    }
}
