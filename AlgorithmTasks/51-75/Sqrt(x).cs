namespace AlgorithmTasks._51_75;

public class Sqrt_x_
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.MySqrt(4);
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int MySqrt(int x)
    {
        if (x == 0)
            return 0;

        int left = 1;
        int right = x;

        while (left <= right) {
            int mid = left + (right - left) / 2;
            int sqrt = x / mid;

            if (sqrt == mid)
                return mid;
            else if (sqrt < mid)
                right = mid - 1;
            else
                left = mid + 1;
        }

        return right;
    }
}