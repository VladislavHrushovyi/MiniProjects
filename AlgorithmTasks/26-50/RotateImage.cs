namespace AlgorithmTasks._26_50;

public class RotateImage
{
    public void Execute()
    {
        var image = new int[][]
        {
            new int[] {1, 2, 3},
            new int[] {4, 5, 6},
            new int[] {7, 8 ,9}
        };
        var sol = new Solution();
        sol.Rotate(image);
        foreach (var line in image)
        {
            Console.WriteLine(string.Join(", ", line));
        }
    }
}

partial class Solution {
    public void Rotate(int[][] matrix) {
        
    }
}