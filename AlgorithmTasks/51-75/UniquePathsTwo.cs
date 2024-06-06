namespace AlgorithmTasks._51_75;

public class UniquePathsTwo
{
    public void Execute()
    {
        var board = new int[][]
        {
            new int[] { 0, 0, 0 },
            new int[] { 0, 1, 0 },
            new int[] { 0, 0, 0 }
        };
        var sol = new Solution();
        var result = sol.UniquePathsWithObstacles(board);
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        return int.MaxValue;
    }
}