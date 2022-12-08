namespace day8;

public class Program
{
    private static async Task Main(string[] args)
    {
        var input = await File.ReadAllLinesAsync("./input.txt");
        // var first = SolveProblem1(input);
        var second = SolveProblem2(input);
        if (second.ScenicScore >= 368368)
            throw new Exception();

        // Console.WriteLine($"First: {first}");
        Console.WriteLine($"Second: {second.ScenicScore}");
    }

    public static int SolveProblem1(string[] input) {
        var trees = new List<Tree>();
        for (int rowIndex = 0; rowIndex < input.Length; rowIndex++)
        {
            var treeRow = input[rowIndex].Trim().TrimEnd('\r');

            for (int columnIndex = 0; columnIndex < treeRow.Length; columnIndex++)
            {
                var height = Int32.Parse($"{treeRow[columnIndex]}");
                trees.Add(new Tree() {
                    Height = height,
                    Location = (rowIndex, columnIndex)
                });
            }
        }

        foreach (var tree in trees) {

            if (tree.Location == (2,1)) {
                var t = 1;
            }
            
            var treesUp = trees.Where(t => t.Location.row < tree.Location.row && t.Location.column == tree.Location.column).ToList();
            var treesDown = trees.Where(t => t.Location.row > tree.Location.row && t.Location.column == tree.Location.column).ToList();
            var treesLeft = trees.Where(t => t.Location.column < tree.Location.column && t.Location.row == tree.Location.row).ToList();
            var treesRight = trees.Where(t => t.Location.column > tree.Location.column && t.Location.row == tree.Location.row).ToList();
            if (!treesUp.Any() || !treesDown.Any() || !treesLeft.Any() || !treesRight.Any())
            {
                // the tree is on the edge and so is visible
                tree.IsVisible = true;
                continue;
            }

            if (treesUp.Any(t => t.Height >= tree.Height) &&
            treesDown.Any(t => t.Height >= tree.Height) &&
            treesLeft.Any(t => t.Height >= tree.Height) &&
            treesRight.Any(t => t.Height >= tree.Height))
            {
                continue;
            }

            tree.IsVisible = true;
        }

        var result = trees.Where(t => t.IsVisible).Count();

        return result;
    }

    public static Tree SolveProblem2(string[] input) {
        var trees = new List<Tree>();
        var grid = new List<List<Tree>>();
        for (int rowIndex = 0; rowIndex < input.Length; rowIndex++)
        {
            var treeRow = input[rowIndex].Trim().TrimEnd('\r');
            grid.Add(new List<Tree>());

            for (int columnIndex = 0; columnIndex < treeRow.Length; columnIndex++)
            {
                var height = Int32.Parse($"{treeRow[columnIndex]}");
                var tree = new Tree() {
                    Height = height,
                    Location = (rowIndex, columnIndex)
                };
                trees.Add(tree);
                
                grid[rowIndex].Add(tree);
            }
        }

        Tree bestTree = null;

        foreach (var tree in trees) {
            var treeUp = trees
                .Where(t => t.Location.row < tree.Location.row && t.Location.column == tree.Location.column)
                .OrderByDescending(t1 => t1.Location.row)
                .ToList();
            var treeDown = trees
                .Where(t => t.Location.row > tree.Location.row && t.Location.column == tree.Location.column)
                .OrderBy(t1 => t1.Location.row)
                .ToList();
            var treeLeft = trees
                .Where(t => t.Location.column < tree.Location.column && t.Location.row == tree.Location.row)
                .OrderByDescending(t1 => t1.Location.column)
                .ToList();
            var treeRight = trees
                .Where(t => t.Location.column > tree.Location.column && t.Location.row == tree.Location.row)
                .OrderBy(t1 => t1.Location.column)
                .ToList();
            if (!treeUp.Any() || !treeDown.Any() || !treeLeft.Any() || !treeRight.Any())
            {
                // the tree is on the edge and so one of the multipliers will be 0
                tree.ScenicScore = 0;
                continue;
            }

            var up = 0;
            foreach(var t in treeUp) {
                up++;
                if (t.Height >= tree.Height)
                    break;
            }

            var down = 0;
            foreach(var t in treeDown) {
                down++;
                if (t.Height >= tree.Height)
                    break;
            }

            var left = 0;
            foreach(var t in treeLeft) {
                left++;
                if (t.Height >= tree.Height)
                    break;
            }

            var right = 0;
            foreach(var t in treeRight) {
                right++;
                if (t.Height >= tree.Height)
                    break;
            }

            tree.ScenicScore = up * down * left * right;
            if (tree.Location == (52,14)) {
                var t = 1;
            }

            if (bestTree == null)
            {
                bestTree = tree;
                continue;
            }
            
            if (tree.ScenicScore > bestTree.ScenicScore)
                bestTree = tree;
        }

        return bestTree;
    }
}

public class Tree {
    public (int row, int column) Location { get; set; }
    public int Height { get; set; }
    public bool IsVisible { get ; set; } = false;
    public long ScenicScore { get; set; }
}