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
        IList<int> res = new List<int>();
        int top = 0, left = 0, right = matrix[0].Length - 1, bot = matrix.Length - 1, elements = matrix.Length * matrix[0].Length;

        while (res.Count < elements) {
            for (int i = left; i <= right && res.Count < elements; i++) {
                res.Add(matrix[top][i]);
            }
            top++;

            for (int j = top; j <= bot && res.Count < elements; j++) {
                res.Add(matrix[j][right]);
            }
            right--;

            for (int i = right; i >= left && res.Count < elements; i--) {
                res.Add(matrix[bot][i]);
            }
            bot--;

            for (int i = bot; i >= top && res.Count < elements; i--) {
                res.Add(matrix[i][left]);
            }

            left++;
        }

        return res;
    }
}