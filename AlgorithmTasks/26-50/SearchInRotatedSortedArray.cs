namespace AlgorithmTasks._26_50;

public class SearchInRotatedSortedArray
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.Search(new[] { 3, 1 }, 1);
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int Search(int[] nums, int target)
    {
        int left = 0, right = nums.Length - 1;
        while (left < right)
        {
            int mid = (left + right) >> 1;  // / 2

            if (nums[0] <= nums[mid])
            {
                if (nums[0] <= target && target <= nums[mid])
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            if (nums[mid] < target && target <= nums[nums.Length - 1])
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
            
        }
        return nums[left] == target ? left : -1;
    }
}