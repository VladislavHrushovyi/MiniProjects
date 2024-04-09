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
        if (nums[0] == target)
        {
            return 0;
        }
        if (nums.Length >= 1)
        {
            int index = nums.ElementAt(target);
            return index == nums.Length ? -1 : index;
        }
        while (left < right)
        {
            int mid = (right + left) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }

            if (nums[left] <= nums[mid])
            {
                if (nums[left] <= target && target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            {
                if (nums[mid] < target && target <= nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }

        return -1;
    }
}