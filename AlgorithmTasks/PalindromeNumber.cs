namespace AlgorithmTasks;

public class PalindromeNumber
{
    public void Execute()
    {
        var solution = new Solution();
        var result = solution.IsPalindrome(2221);
        Console.WriteLine(result);
    }
}

partial class Solution {
    public bool IsPalindrome(int x)
    {
        if (x == 0)
        {
            return true;
        }
        if (x < 0)
        {
            return false;
        }
        if (x <= 9)
        {
            return true;
        }

        var orders = 0;
        var xCopy = x;
        while (xCopy != 0)
        {
            orders++;
            xCopy /= 10;
        }

        var stack = new Stack<int>();
        var orderIdx = orders;
        while (orderIdx != 0)
        {
            if (orders % 2 == 0)
            {
                if (orderIdx > orders / 2)
                {
                    int item = x % 10;
                    stack.Push(item);
                    x /= 10;
                    orderIdx--;
                }
                else
                {
                    var item = stack.Pop();
                    var partNumber = x % 10;
                    if (item - partNumber == 0)
                    {
                        x /= 10;
                        orderIdx--;
                    }
                    else
                    {
                        return false;
                    }
                }   
            }
            else
            {
                var mid = orders / 2 + 1;
                if (orderIdx > mid)
                {
                    int item = x % 10;
                    stack.Push(item);
                    x /= 10;
                    orderIdx--;
                }
                else if (orderIdx < mid)
                {
                    var item = stack.Pop();
                    var partNumber = x is > 0 and <= 9 ? x : x % 10  ;
                    if (item - partNumber == 0)
                    {
                        orderIdx--;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    orderIdx--;
                    x /= 10;
                }
            }
        }
        return true;
    }
}