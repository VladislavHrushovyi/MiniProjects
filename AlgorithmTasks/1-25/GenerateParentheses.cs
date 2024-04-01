namespace AlgorithmTasks;

public class GenerateParentheses
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.GenerateParenthesis(8);
        
        Console.WriteLine(string.Join(",", result));
    }
}

partial class Solution {
    public IList<string> GenerateParenthesis(int n)
    {
        var result = new List<string>();
        Generate(result, 0, 0, "", n);

        return result;
    }

    private void Generate(List<string> result, int left, int right, string current, int n)
    {
        if (current.Length == 2 * n)
        {
            result.Add(current);
            return;
        }

        if (left < n)
        {
            Generate(result, left + 1, right, current + '(', n);
        }
        
        if (right < left)
        {
            Generate(result, left, right + 1, current + ')', n);
        }
    }
}