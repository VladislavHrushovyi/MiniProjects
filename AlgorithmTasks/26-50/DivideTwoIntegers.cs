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
        if(dividend == int.MinValue && divisor == -1) return int.MaxValue;
        int sign = (dividend < 0) ^ (divisor < 0)? -1:1;
        long dvd = Math.Abs((long)dividend);
        long dvs = Math.Abs((long)divisor);
        long quotient = 0;
        for(int i = 31; i >= 0; i--){
            if((dvd >> i) >= dvs){
                quotient += (1L << i);
                dvd -= (dvs << i);
            }
        }
        return (int)(sign * quotient);
    }
}