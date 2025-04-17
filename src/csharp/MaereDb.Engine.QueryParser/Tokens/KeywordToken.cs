namespace MaereDb.Engine.QueryParser.Tokens;

internal record KeywordToken(string Value, Keyword Keyword) : Token(Value, TokenType.Keyword);

internal enum Keyword
{
    Select,
    From,
    Where,
    Insert,
    Update,
    Delete,
    Create,
    Drop,
    Alter,
    Table,
    Column,
    Index,
    View,
    Join,
    InnerJoin,
    LeftJoin,
    RightJoin,
    FullJoin,
    CrossJoin,
}