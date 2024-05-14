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
        int count = 0;
        var rows = new bool[n];
        var diag1 = new bool[2 * n];
        var diag2 = new bool[2 * n];

        Helper(0);

        return count;

        void Helper(int col)
        {
            if (col == n) count++;

            for (int row = 0; row < n; row++)
            {
                int a = row + col;
                int b = row - col + n;

                if (rows[row] || diag1[a] || diag2[b]) continue; 

                rows[row] = diag1[a] = diag2[b] = true; 
                Helper(col + 1);
                rows[row] = diag1[a] = diag2[b] = false;
            }
        }
    }
}