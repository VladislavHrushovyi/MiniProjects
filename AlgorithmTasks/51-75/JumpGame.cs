namespace AlgorithmTasks._51_75;

public class JumpGame
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.CanJump(new[] { 2, 3, 1, 1, 4 });
        Console.WriteLine(result);
    }
}

partial class Solution {
    public bool CanJump(int[] nums)
    {
        int finishIndex = nums.Length - 1;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            if (i + nums[i] >= finishIndex)
            {
                if (i == 0) return true;
                finishIndex = i;
            }
        }
        return false;
    }
}