namespace AlgorithmTasks._51_75;

public class MinimumPathSum
{
    public void Execute()
    {
        var grid = new int[][]
        {
            new[] { 1, 3, 1 },
            new[] { 1, 5, 1 },
            new[] { 4, 2, 1 }
        };
        var sol = new Solution();
        var result = sol.MinPathSum(grid);
        Console.WriteLine(result);
    }
}

partial class Solution
{
    public int MinPathSum(int[][] grid)
    {
        return 0;
    }
}