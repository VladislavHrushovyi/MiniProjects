namespace AlgorithmTasks;

public class BiggestWaterContainer
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.MaxArea(new[] {1,1});
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int MaxArea(int[] height) {
        int maxArea = 0;
        int left = 0, right = height.Length - 1;

        while (left < right)
        {
            int tempHeight = 0;

            int width = right - left;

            if (height[right] > height[left])
            {
                tempHeight = height[left];
                left++;
            }
            else
            {
                tempHeight = height[right];
                right--;
            }

            int area = tempHeight * width;

            maxArea = Math.Max(area, maxArea);
        }

        return maxArea;
    }
}