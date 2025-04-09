using MaereDb.Tests.Examples;

namespace MaereDb.Tests.InnerCircle;

public class UseCase01
{
    [Fact]
    public void GivenTwoNumbers_WhenSentToDatabaseInMemory_SumOfthemReturned()
    {
        // Arrange
        var expected = 4;
        var a = 2;
        var b = 2;
        var math = new MathOperations(new InMemoryDbEngine());

        // Act
        var result = math.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
}
