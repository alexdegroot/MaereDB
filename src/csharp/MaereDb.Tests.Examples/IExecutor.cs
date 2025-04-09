namespace MaereDb.Tests.Examples;

public interface IExecutor
{
    object ExecuteScalar(string sql);
}
