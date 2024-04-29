using System.Text;

namespace AlgorithmTasks._26_50;

public class Permutations
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.Permute(new[] { 1,2,3 });
        var sb = new StringBuilder();
        foreach (var row in result)
        {
            sb.Append($"[{string.Join(",", row)}] ");
        }
        
        Console.WriteLine(sb);
    }
}

partial class Solution {
    public IList<IList<int>> Permute(int[] nums)
    {
        return new List<IList<int>>();
    }
}