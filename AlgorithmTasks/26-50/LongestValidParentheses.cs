namespace AlgorithmTasks._26_50;

public class LongestValidParentheses
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.LongestValidParentheses(")()())");
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int LongestValidParentheses(string s)
    {
        int maxValid = 0;
        int n = s.Length;
        Stack<int> stack = new Stack<int>();
        stack.Push(-1);

        for (int i = 0; i < n; i++)
        {
            if (s[i] == '(')
            {
                stack.Push(i);
            }
            else
            {
                stack.Pop();
                if (stack.Count != 0)
                {
                    maxValid = Math.Max(maxValid, i - stack.Peek());
                }
                else
                {
                    stack.Push(i);
                }
            }
        }
        return maxValid;
    }
}