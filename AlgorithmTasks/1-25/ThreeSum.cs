using System.Text;

namespace AlgorithmTasks;

public class ThreeSum 
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.ThreeSum(new[] { -1, 0, 1, 2, -1, -4, 0, 0, 0 });
        var sbResult = new StringBuilder();

        sbResult.Append('[');
        foreach (var lines in result)
        {
            sbResult.Append($"[{string.Join(',', lines)}],");
        }
        sbResult.Append(']');
        
        Console.WriteLine(sbResult);
    }
}

partial class Solution {
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        if (nums.Length <= 2) return new List<IList<int>>();
        var result = new List<IList<int>>();
        Array.Sort(nums);
        int start = 0, left, right;
        int target = 0;

        while (start < nums.Length - 2)
        {
            target = nums[start] * -1;
            left = start + 1;
            right = nums.Length - 1;

            while (left < right)
            {
                if (target < nums[left] + nums[right])
                {
                    right--;
                }
                else if(target > nums[left] + nums[right])
                {
                    left++;
                }
                else
                {
                    var threePairs = new List<int>() { nums[start], nums[left], nums[right] };
                    result.Add(threePairs);
                    
                    while (left < right && nums[right] == threePairs[2])
                    {
                        right--;
                    }
                    while (left < right && nums[left] == threePairs[1])
                    {
                        left++;
                    }
                }
            }

            int currStart = nums[start];
            while (start < nums.Length - 2 && nums[start] == currStart)
            {
                ++start;
            }
        }

        return result;
    }
}