namespace AlgorithmTasks._51_75;

public class SpiralMatrixTwo
{
    public void Execute()
    {
        int n = 3;
        var sol = new Solution();
        var result = sol.GenerateMatrix(n);
        
        Console.Write("[");
        foreach (var line in result)
        {
            Console.Write("["+string.Join(",", line)+"]");
        }
        Console.WriteLine("]");
    }
}

partial class Solution {
    public int[][] GenerateMatrix(int n)
    {
        return new[] { new[] { 1 } };
    }
}