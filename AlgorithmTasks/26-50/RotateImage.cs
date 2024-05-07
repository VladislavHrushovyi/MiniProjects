namespace AlgorithmTasks._26_50;

public class RotateImage
{
    public void Execute()
    {
        var image = new int[][]
        {
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6 },
            new int[] { 7, 8, 9 }
        };
        var sol = new Solution();
        sol.Rotate(image);
        foreach (var line in image)
        {
            Console.WriteLine(string.Join(", ", line));
        }
    }
}

partial class Solution
{
    public void Rotate(int[][] matrix)
    {
        for(int i=0;i<matrix.GetLength(0)-1;i++){
            for(int j= i+1;j<matrix[i].Length;j++){
                (matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);
            }
        }
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            int start = 0;
            int end = matrix[i].Length - 1;
            while (start < end)
            {
                (matrix[i][start], matrix[i][end]) = (matrix[i][end], matrix[i][start]);
                start++;
                end--;
            }
        }
    }
}