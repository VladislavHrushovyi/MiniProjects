using System.Diagnostics;

namespace AlgorithmTasks;

public class ReverseInteger
{
    public void Execute()
    {
        var solution = new Solution();
        var result = solution.Reverse(-2147483648);
        Console.WriteLine(result);
        Debug.Assert(result == 0);
    }
}

public partial class Solution {
    public int Reverse(int x)
    {
        if (x == 0)
        {
            return 0;
        }

        if (x == int.MaxValue || x == int.MinValue)
        {
            return 0;
        }
        var sign = x < 0 ? -1 : 1;
        x = Math.Abs(x);
        var result = 0;
        while (x != 0)
        {
            var remainder = x % 10;

            if (result > int.MaxValue / 10 || (result == int.MaxValue / 10 && remainder > 7))
            {
                return 0;
            }
            
            var temp = result * 10 + remainder;
            result = temp;

            x /= 10;
        }
        
        return result * sign;
    }
}