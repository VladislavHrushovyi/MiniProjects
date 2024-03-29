namespace AlgorithmTasks;

public class ValidParentheses
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.IsValid("()[]{}");
        Console.WriteLine(result);
    }
}

partial class Solution {
    public bool IsValid(string s)
    {
        if (string.IsNullOrEmpty(s) || s.Length == 1)
        {
            return false;
        }

        Stack<char> parantheses = new Stack<char>();
        foreach (var symbol in s)
        {
            if (symbol is '(' or '{' or '[')
            {
                parantheses.Push(symbol);
            }
            else if(parantheses.Count == 0 || Math.Abs(symbol - parantheses.Peek()) > 2)
            {
                return false;
            }
            else
            {
                parantheses.Pop();
            }
        }
        
        return parantheses.Count == 0;
    }
}