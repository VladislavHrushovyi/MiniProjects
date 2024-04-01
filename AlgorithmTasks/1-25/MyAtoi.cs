namespace AlgorithmTasks;

public class MyAtoi
{
    public void Execute()
    {
        var solution = new Solution();
        var result = solution.MyAtoi("2147483646");
        Console.WriteLine(result);
    }
}
partial class Solution {
    public int MyAtoi(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return 0;
        }
        
        s = s.Trim();
        int result = 0;
        int idx = 0;
        int sign = 1;

        if (s[idx] == '-' || s[idx] == '+')
        {
            sign = s[idx++] == '-' ? -1 : 1;
        }
        
        if (idx >= s.Length)
        {
            return 0;
        }

        while (idx < s.Length && IsDigit(s[idx]))
        {
            int currDigit = s[idx] - '0';
            
            if (result > int.MaxValue / 10 || (result == int.MaxValue / 10 && currDigit > 7))
            {
                return sign == 1 ? int.MaxValue : int.MinValue;
            }
            result = result * 10 + currDigit;
            idx++;
        }
        return result * sign;
    }

    private bool IsDigit(char symbol)
    {
        return (uint)(symbol - '0') <= (uint)('9' - '0');
    }
}