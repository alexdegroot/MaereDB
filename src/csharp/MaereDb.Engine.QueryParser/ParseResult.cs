using MaereDb.Engine.QueryParser.Aqt;

namespace MaereDb.Engine.QueryParser;

internal record ParseResult(bool Success, IList<IStatement>? Statements, string? ErrorMessage)
{
    public static ParseResult CreateFailure(string errorMessage) => new ParseResult(false, null, errorMessage);
    public static ParseResult CreateSuccess(IList<IStatement> statements) => new ParseResult(true, statements, null);
}
