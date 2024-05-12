using System.Text;

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
        var result = new List<IList<string>>();
        var board = new List<StringBuilder>();
        for (var i = 0; i < n; i++)
        {
            board.Add(new());
            board[i].Append('.', n);
        }


        void Dfs(int row, HashSet<int> cols, HashSet<int> posDiag, HashSet<int> negDiag)
        {
            if (row == n)
            {
                result.Add(board.Select(x => x.ToString()).ToList());
                return;
            }

            for (var col = 0; col < n; col++)
            {
                if (cols.Contains(col) ||
                    posDiag.Contains(row + col) ||
                    negDiag.Contains(row - col))
                {
                    continue;
                }

                cols.Add(col);
                posDiag.Add(row + col);
                negDiag.Add(row - col);
                board[row][col] = 'Q';
                Dfs(row + 1, cols, posDiag, negDiag);
                board[row][col] = '.';
                cols.Remove(col);
                posDiag.Remove(row + col);
                negDiag.Remove(row - col);
            }
        }

        Dfs(0, new HashSet<int>(), new HashSet<int>(), new HashSet<int>());
        return result;
    }
}