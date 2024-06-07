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

partial class Solution
{
    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        int m = obstacleGrid.Length;
        int n = obstacleGrid[0].Length;

        if (obstacleGrid[0][0] == 1 || obstacleGrid[m - 1][n - 1] == 1)
        {
            return 0;
        }

        int[,] dp = new int[m, n];
        dp[0, 0] = 1;

        for (int i = 1; i < m; i++)
        {
            if (obstacleGrid[i][0] == 0)
            {
                dp[i, 0] = dp[i - 1, 0];
            }
        }

        for (int j = 1; j < n; j++)
        {
            if (obstacleGrid[0][j] == 0)
            {
                dp[0, j] = dp[0, j - 1];
            }
        }

        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                if (obstacleGrid[i][j] == 0)
                {
                    dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                }
            }
        }

        return dp[m - 1, n - 1];
    }
}