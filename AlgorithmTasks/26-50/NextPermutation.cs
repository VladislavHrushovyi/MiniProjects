namespace AlgorithmTasks._26_50;

public class NextPermutation
{
    public void Execute()
    {
        var sol = new Solution();
        int[] nums = new[] { 3, 2, 1 };
        sol.NextPermutation(nums);
        
        Console.WriteLine(string.Join(",", nums));
    }
}

partial class Solution {
    public void NextPermutation(int[] nums)
    {
        int p1 = -1;
        int p2 = -1;

        for (int i = nums.Length - 2; i >= 0; i--)
        {
            if (nums[i] < nums[i + 1])
            {
                p1 = i;
                break;
            }
        }

        if (p1 < 0)
        {
            Array.Reverse(nums);
        }
        else
        {
            for (int i = nums.Length - 1; i >=0; i--)
            {
                if (nums[i] > nums[p1])
                {
                    p2 = i;
                    break;
                }
            }
            
            Swap(nums, p1,p2);
            Reverse(nums, p1 + 1);
        }
    }

    private void Swap(int[] nums, int p1, int p2)
    {
        (nums[p1], nums[p2]) = (nums[p2], nums[p1]);
    }

    private void Reverse(int[] nums, int start)
    {
        int p1 = start;
        int p2 = nums.Length - 1;

        while (p1 < p2)
        {
            Swap(nums, p1, p2);
            p1++;
            p2--;
        }
    }
}