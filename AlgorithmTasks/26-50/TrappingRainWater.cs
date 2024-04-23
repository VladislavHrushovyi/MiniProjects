﻿namespace AlgorithmTasks._26_50;

public class TrappingRainWater
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.Trap(new []{0,1,0,2,1,0,1,3,2,1,2,1});
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int Trap(int[] height)
    {
        int n = height.Length;
        if (n == 0) return 0;
        
        int left = 0, right = n - 1;
        int leftMax = 0, rightMax = 0;
        int waterTrapped = 0;
        
        while (left < right) {
            if (height[left] < height[right]) {
                if (height[left] >= leftMax) {
                    leftMax = height[left];
                } else {
                    waterTrapped += leftMax - height[left];
                }
                left++;
            } else {
                if (height[right] >= rightMax) {
                    rightMax = height[right];
                } else {
                    waterTrapped += rightMax - height[right];
                }
                right--;
            }
        }
        
        return waterTrapped;
    }
}