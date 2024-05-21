namespace AlgorithmTasks._51_75;

public class MaximumSubarray
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.MaxSubArray(new[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 });
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int MaxSubArray(int[] nums) {
        return DivideAndConquer(nums, 0, nums.Length - 1);
    }
    
    private int DivideAndConquer(int[] nums, int left, int right) {
        if (left == right) return nums[left];
        
        int mid = left + (right - left) / 2;
        
        int leftMax = DivideAndConquer(nums, left, mid);
        int rightMax = DivideAndConquer(nums, mid + 1, right);
        
        int crossingMax = MaxCrossingSubArray(nums, left, mid, right);
        
        return Math.Max(Math.Max(leftMax, rightMax), crossingMax);
    }
    
    private int MaxCrossingSubArray(int[] nums, int left, int mid, int right) {
        int leftSum = 0;
        int leftMaxSum = int.MinValue;
        
        for (int i = mid; i >= left; i--) {
            leftSum += nums[i];
            leftMaxSum = Math.Max(leftMaxSum, leftSum);
        }
        
        int rightSum = 0;
        int rightMaxSum = int.MinValue;
        
        for (int i = mid + 1; i <= right; i++) {
            rightSum += nums[i];
            rightMaxSum = Math.Max(rightMaxSum, rightSum);
        }
        
        return leftMaxSum + rightMaxSum;
    }
}