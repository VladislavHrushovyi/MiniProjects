namespace AlgorithmTasks._26_50;

public class SearchInsertPosition
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.SearchInsert(new[] {1,3,5,6}, 2);
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int SearchInsert(int[] nums, int target)
    {
        if (target > nums[^1])
        {
            return nums.Length;
        }

        int left = 0, right = nums.Length - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }

            if (nums[mid] > target)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }
        return left;
    }
}