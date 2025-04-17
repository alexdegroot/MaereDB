// using MaereDb.Engine.QueryParser.Abstractions;

// namespace MaereDb.Engine.QueryParser.Tests;

// public class SelectQueries
// {
//     [Fact]
//     public void ParseExpressionField()
//     {
//         var sql = "SELECT 1+1 as Sum";
//         var parser = new Parser();

//         var statements = parser.Parse(sql.AsSpan());

//         Assert.NotNull(statements);
//         var statement = Assert.Single(statements);
//         var selectStatement = Assert.IsType<SelectStatement>(statement);
//         var field = Assert.Single(selectStatement.Fields);
//         var expressionField = Assert.IsType<ExpressionField>(field);
//         Assert.Equal("Sum", expressionField.Alias);
//     }

//     [Fact]
//     public void ParseWildcardField()
//     {
//         var sql = "SELECT * FROM Table";
//         var parser = new Parser();

//         var statements = parser.Parse(sql.AsSpan());

//         Assert.NotNull(statements);
//         var statement = Assert.Single(statements);
//         var selectStatement = Assert.IsType<SelectStatement>(statement);
//         var field = Assert.Single(selectStatement.Fields);
//         Assert.IsType<WildcardField>(field);
//     }

//     [Fact]
//     public void ParseWildcardFieldWithReference()
//     {
//         var sql = "SELECT tablename.* FROM Table";
//         var parser = new Parser();

//         var statements = parser.Parse(sql.AsSpan());

//         Assert.NotNull(statements);
//         var statement = Assert.Single(statements);
//         var selectStatement = Assert.IsType<SelectStatement>(statement);
//         var field = Assert.Single(selectStatement.Fields);
//         var wildcardField = Assert.IsType<WildcardField>(field);
//         Assert.Equal("tablename", wildcardField.TableName);
//     }
// }
