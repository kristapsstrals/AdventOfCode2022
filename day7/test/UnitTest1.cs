namespace test;
using day7;

public class UnitTest1
{
    [Fact]
    public void Problem1Test()
    {
        var input = testData.Split(Environment.NewLine);
        var result = Program.SolveFirstProblem(input);
        Assert.Equal(95437, result);
    }

    [Fact]
    public void Problem2Test()
    {
        var input = testData.Split(Environment.NewLine);
        var result = Program.SolveSecondProblem(input);
        Assert.Equal(24933642, result);
    }

    string testData = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";
}