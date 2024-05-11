namespace AlgorithmTasks._51_75;

public class NQueens
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.SolveNQueens(4);
        foreach (var line in result)
        {
            Console.WriteLine("(" + string.Join(",", line) + ")");
        }
    }
}

partial class Solution {
    public IList<IList<string>> SolveNQueens(int n)
    {
        return new List<IList<string>>();
    }
}