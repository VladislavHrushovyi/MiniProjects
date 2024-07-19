namespace AlgorithmTasks._51_75;

public class EditDistance
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.MinDistance("horse", "ros");
        Console.WriteLine(result);
    }
}

partial class Solution
{
    public int MinDistance(string word1, string word2)
    {
        var m = word1.Length;
        var n = word2.Length;

        var dp = new int[m + 1, n + 1];

        for (int i = 0; i <= m; i++)
        {
            dp[i, 0] = i;
        }

        for (int j = 0; j <= n; j++)
        {
            dp[0, j] = j;
        }

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1];
                }
                else
                {
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j], dp[i, j - 1]), dp[i - 1, j - 1]) + 1;
                }
            }
        }

        return dp[m, n];
    }
}