namespace AlgorithmTasks._51_75;

public class NQueensTwo
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.TotalNQueens(4);
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int TotalNQueens(int n)
    {
        int result = 0;
        return result;
    }
}