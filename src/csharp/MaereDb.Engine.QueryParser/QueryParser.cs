namespace MaereDb.Engine.QueryParser;

public class QueryParser
{
        public IList<IStatement> Parse(ReadOnlySpan<char> sql)
        {
            if(sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            {
                return ParseSelect(sql);
            }
            else
            {
                throw new NotSupportedException($"SQL statement not supported: {sql.ToString()}");
            }
        }

        public IList<IStatement> ParseSelect(ReadOnlySpan<char> sql)
        {
            return new [] { new  SelectStatement() };
        }
}

public interface IStatement
{

}

public class SelectStatement : IStatement
{
}



