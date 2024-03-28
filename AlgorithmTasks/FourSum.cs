using System.Text;

namespace AlgorithmTasks;

public class FourSum
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.FourSum(new[] { -3,-1,0,2,4,5 }, 0);
        var sb = new StringBuilder();
        sb.Append('[');
        foreach (var row in result)
        {
            sb.Append($"[{string.Join(',',row )}]");
        }
        sb.Append(']');
        Console.WriteLine(sb);
    }
}

partial class Solution {
    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        var result = new List<IList<int>>();
        Array.Sort(nums);
        for (int i = 0; i < nums.Length - 3; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }

            for (int j = i + 1; j < nums.Length - 2; j++)
            {
                if (j > (i + 1) && nums[j] == nums[j - 1])
                {
                    continue;
                }

                int left = j + 1;
                int right = nums.Length - 1;
                while (left < right)
                {
                    var sum = (long)nums[i] + nums[j] + nums[left] + nums[right];
                    if (target == sum)
                    {
                        var item = new List<int>() { nums[i], nums[j], nums[left], nums[right] };
                        result.Add(item);
                        while (left < right && nums[left] == nums[left + 1])
                        {
                            left++;
                        }
                        while (left < right && nums[right] == nums[right - 1])
                        {
                            right--;
                        }

                        left++;
                        right--;
                    }
                    else if(sum > target)
                    {
                        right--;
                    }
                    else
                    {
                        left++;
                    }
                }
            }
        }
        return result;
    }
}