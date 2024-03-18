using System.Text;

namespace AlgorithmTasks;

public class ZigzagConversion
{
    public void Exectute()
    {
        var solution = new Solution();
        var result = solution.Convert("PAYPALISHIRING", 3);
        
        Console.WriteLine(result == "PAHNAPLSIIGYIR");
    }
}

public partial class Solution {
    public string Convert(string s, int numRows)
    {
        if (numRows < 2)
        {
            return s;
        }
        var rows = Enumerable.Range(0, numRows).Select(_ => new StringBuilder()).ToArray();
        var currRow = 0;
        var direction = -1;
        foreach (var symbol in s)
        {
            rows[currRow].Append(symbol);
            currRow += (direction == -1) ? 1 : -1;

            if (currRow == numRows - 1 || currRow == 0)
            {
                direction = -direction;
            }
        }
        
        string result = String.Empty;
        foreach (var row in rows)
        {
            result += row;
        }

        return result;
    }
}