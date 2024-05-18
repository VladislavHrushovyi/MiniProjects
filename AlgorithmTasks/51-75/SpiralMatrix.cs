namespace AlgorithmTasks._51_75;

public class SpiralMatrix
{
    public void Execute()
    {
        var data = new int[][]
        {
            new[] { 1, 2, 3 },
            new[] { 4, 5, 6 },
            new[] { 7, 8, 9 },
        };
        var sol = new Solution();
        var result = sol.SpiralOrder(data);
        Console.WriteLine(string.Join(",", result));
    }
}

partial class Solution {
    public IList<int> SpiralOrder(int[][] matrix)
    {
        return new List<int>();
    }
}