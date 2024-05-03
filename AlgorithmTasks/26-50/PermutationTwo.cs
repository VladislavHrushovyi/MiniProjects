using System.Text;

namespace AlgorithmTasks._26_50;

public class PermutationTwo
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.PermuteUnique(new[] { 1, 1, 2 });
        var sb = new StringBuilder();
        foreach (var row in result)
        {
            sb.Append($"[{string.Join(",", row)}] ");
        }

        Console.WriteLine(sb);
    }
}

partial class Solution {
    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        IList<IList<int>> result = new List<IList<int>>();
        Backtrack(nums, 0, result);
        return result;
    }
    
    private void Backtrack(IList<int> nums, int begin, IList<IList<int>> result)
    {
        if (begin == nums.Count - 1)
        {
            result.Add(nums.ToArray());
        }

        var set = new HashSet<int>();

        for (int i = begin; i < nums.Count; i++)
        {
            if (!set.Add(nums[i])) continue;

            (nums[i], nums[begin]) = (nums[begin], nums[i]);

            Backtrack(nums, begin + 1, result);

            (nums[i], nums[begin]) = (nums[begin], nums[i]);
        }
    }
}