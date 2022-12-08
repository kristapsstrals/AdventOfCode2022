namespace test;

public class UnitTest1
{
    [Fact]
    public void TestProblem1()
    {
        var input = TestData.Split(Environment.NewLine);
        var result = day8.Program.SolveProblem1(input);
        Assert.Equal(21, result);
    }

    [Fact]
    public void TestProblem2()
    {
        var input = TestData.Split(Environment.NewLine);
        var result = day8.Program.SolveProblem2(input);
        Assert.Equal(8, result?.ScenicScore);
    }

    string TestData = @"30373
25512
65332
33549
35390";
}