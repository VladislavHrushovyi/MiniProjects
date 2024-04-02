namespace AlgorithmTasks._26_50;

public class RemoveDuplicatesFromSortedArray
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.RemoveDuplicates(new[] { 1, 2, 3, 4, 4, 4, 5, 6, 6, 6, 6 });
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int RemoveDuplicates(int[] nums) {
        if (nums.Length == 0)
        {
            return 0;
        }

        int j = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] != nums[j])
            {
                nums[++j] = nums[i];
            }
        }

        return j+1;
    }
}