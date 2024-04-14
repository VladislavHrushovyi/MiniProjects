namespace AlgorithmTasks._26_50;

public class SudokuSolver
{
    public void Execute()
    {
        var sol = new Solution();
        var board = new[]
        {
            new[] { "5", "3", ".", ".", "7", ".", ".", ".", "." },
            new[] { "6", ".", ".", "1", "9", "5", ".", ".", "." },
            new[] { ".", "9", "8", ".", ".", ".", ".", "6", "." },
            new[] { "8", ".", ".", ".", "6", ".", ".", ".", "3" },
            new[] { "4", ".", ".", "8", ".", "3", ".", ".", "1" },
            new[] { "7", ".", ".", ".", "2", ".", ".", ".", "6" },
            new[] { ".", "6", ".", ".", ".", ".", "2", "8", "." },
            new[] { ".", ".", ".", "4", "1", "9", ".", ".", "5" },
            new[] { ".", ".", ".", ".", "8", ".", ".", "7", "9" }
        };
        sol.SolveSudoku(board);
        Console.WriteLine($"{string.Join(",", board[0])}");
    }
}

partial class Solution {
    public void SolveSudoku(string[][] board) {
        SolveSudoku(board, 0, 0);
    }

    private bool SolveSudoku(string[][] board, int row, int col)
    {
        if (col == 9)
        {
            col = 0;
            row++;

            if (row == 9)
            {
                return true;
            }
        }

        if (board[row][col] != ".")
        {
            return SolveSudoku(board, row, col + 1);
        }

        for (char i = '1'; i <= '9'; i++)
        {
            if (IsValidSudoku(board, row, col, $"{i}"))
            {
                board[row][col] = $"{i}";
                if (SolveSudoku(board, row, col + 1))
                {
                    return true;
                }
                board[row][col] = ".";
            }
        }
        return false;
    }
    
    private bool IsValidSudoku(string[][] board, int row, int col, string newValue)
    {
        return IsValidRow(board, row, newValue) 
               && IsValidCol(board, col, newValue) 
               && IsValidSquare(board, row, col, newValue);
    }

    private bool IsValidRow(string[][] board, int row, string newValue)
    {
        for (int i = 0; i < board[0].Length; i++)
        {
            if (board[row][i] == newValue)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsValidCol(string[][] board, int col, string newValue)
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i][col] == newValue)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsValidSquare(string[][] board, int row, int col, string newValue)
    {
        int startRow = (row / 3) * 3;
        int startCol = (col / 3) * 3;

        for (int i = startRow; i < startRow + 3; i++)
        {
            for (int j = startCol; j < startCol + 3; j++)
            {
                if (board[i][j] == newValue)
                {
                    return false;
                }
            }
        }

        return true;
    }
}