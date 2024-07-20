namespace AlgorithmTasks._51_75;

public class SetMatrixZeroes
{
    public void Execute()
    {
        var matrix = new int[][]
        {
            new int[] { 1, 1, 1 },
            new int[] { 1, 0, 1 },
            new int[] { 1, 1, 1 }
        };
        var sol = new Solution();
        sol.SetZeroes(matrix);

        Console.Write("[");
        foreach (var line in matrix)
        {
            Console.Write("["+string.Join(", ", line)+"]");
        }
        Console.Write("]");
    }
}

partial class Solution {
    public void SetZeroes(int[][] matrix) {
        
    }
}