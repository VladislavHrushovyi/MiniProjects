namespace AlgorithmTasks._26_50;

public class FirstMissingPositive
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.FirstMissingPositive(new[] { 7, 8, 9, 11, 12 });
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int FirstMissingPositive(int[] nums)
    {
        HashSet<int> h = new HashSet<int>();
        for (int i = 0; i < nums.Length; i++)
            if (nums[i] > 0)
                h.Add(nums[i]);
                
        for (int i = 1;;i++)
        {
            if (!h.Contains(i))
                return i;
        }
    }
}