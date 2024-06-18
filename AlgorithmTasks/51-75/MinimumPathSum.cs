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
        int m = grid.Length, n = grid[0].Length;
        int[,] dp = new int[m,n];
        dp[0,0] = grid[0][0];

        for(int i = 1; i < m; i++)
            dp[i,0] = dp[i-1,0] + grid[i][0];
        
        for(int i = 1; i < n; i++)
            dp[0,i] = dp[0,i-1] + grid[0][i];

        for(int i = 1; i < m; i++){
            for(int j = 1; j < n; j++){
                dp[i,j] = grid[i][j] + Math.Min(dp[i-1,j], dp[i, j-1]);
            }
        }

        return dp[m-1,n-1];
    }
}