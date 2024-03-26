namespace AlgorithmTasks;

public class ClosestThreeSum
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.ThreeSumClosest(new[] { 0,1,2 }, 1);
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int ThreeSumClosest(int[] nums, int target)
    {
        int minValueDifference = int.MaxValue;
        Array.Sort(nums);
        int left, right;
        int result = 0;
        for (int i = 0; i < nums.Length - 2; i++)
        {
            left = i + 1;
            right = nums.Length - 1;
            while (left < right)
            {
                int currValuesResult = nums[i] + nums[left] + nums[right];
                int currDifference = Math.Abs(target - currValuesResult);
                if (minValueDifference > currDifference)
                {
                    result = currValuesResult;
                    minValueDifference = currDifference;
                }

                if (currValuesResult > target)
                {
                    right--;
                }
                else
                {
                    left++;
                }
            }
        }
        return result;
    }
}