namespace AlgorithmTasks._26_50;

public class ValidSudoku
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.IsValidSudoku(new string[][]
        {
            new []{"5","3",".",".","7",".",".",".","."},
            new []{"6",".",".","1","9","5",".",".","."},
            new []{".","9","8",".",".",".",".","6","."},
            new []{"8",".",".",".","6",".",".",".","3"},
            new []{"4",".",".","8",".","3",".",".","1"},
            new []{"7",".",".",".","2",".",".",".","6"},
            new []{".","6",".",".",".",".","2","8","."},
            new []{".",".",".","4","1","9",".",".","5"},
            new []{".",".",".",".","8",".",".","7","9"}
        });
        
        Console.WriteLine(result);
    }
}

partial class Solution {
    public bool IsValidSudoku(string[][] board)
    {
        var set = new HashSet<string>();
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                if (board[i][j] != ".")
                {
                    string item = $"({board[i][j]})";
                    if (!set.Add(item + i) || !set.Add(j + item) || !set.Add(i / 3 + item + j / 3))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}