namespace test;

public class UnitTest1
{
    [Fact]
    public void TestProblem1()
    {
        var input = TestData1.Split(Environment.NewLine);
        var result = day9.Program.SolveProblem(input, 1);
        Assert.Equal(13, result);
    }

    [Theory]
    [InlineData(TestData1, 1)]
    [InlineData(TestData2, 36)]
    public void TestProblem2(string data, int expected)
    {
        var input = data.Split(Environment.NewLine);
        var result = day9.Program.SolveProblem(input, 9);
        Assert.Equal(expected, result);
    }

    const string TestData1 = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";

    const string TestData2 = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20";
}