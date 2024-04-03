namespace AlgorithmTasks._26_50;

public class DivideTwoIntegers
{
    public void Execute()
    {
        var sol = new Solution();
        var result1 = sol.Divide(-2147483648, 2);
        var result2 = sol.Divide(0, 3);
        var result3 = sol.Divide(-10, 3);
        Console.WriteLine(result1);
        Console.WriteLine(result2);
        Console.WriteLine(result3);
    }
}

partial class Solution {
    public int Divide(int dividend, int divisor) {
        if (dividend == 0)
        {
            return 0;
        }

        if (divisor == 1 && dividend == int.MaxValue) return int.MaxValue;
        if (divisor == -1 && dividend == int.MaxValue) return -int.MaxValue;
        if (divisor == 1 && dividend == int.MinValue) return int.MinValue;
        if (divisor == -1 && dividend == int.MinValue) return int.MaxValue;
        int sign = (dividend < 0) ^ (divisor < 0) ? -1 : 1;
        
        long dividendLong = Math.Abs((long)dividend);
        long divisorLong = Math.Abs((long)divisor);
        int parts = 0;
        while (dividendLong >= divisorLong)
        {
            parts++;
            dividendLong -= divisorLong;
        }

        if (sign == -1)
        {
            parts = -parts;
        }

        return parts;
    }
}