namespace AlgorithmTasks._26_50;

public class FindFirstAndLastPositionOfElementInSortedArray
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.SearchRange(new[] { 2,2 }, 2);
        
        Console.WriteLine(string.Join(",", result));
    }
}

partial class Solution {
    public int[] SearchRange(int[] nums, int target)
    {
        int first = BinarySearch(nums, target, false);
        int last = BinarySearch(nums, target, true);
        
        return new []{first, last};
    }

    private int BinarySearch(int[] nums, int target, bool isLeftDirection)
    {
        int left = 0, right = nums.Length - 1;
        int index = -1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                index = mid;
                if (isLeftDirection)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }else if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return index;
    }
}