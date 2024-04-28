namespace AlgorithmTasks._26_50;

public class JumpGameTwo
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.Jump(new[] { 2, 3, 1, 1, 4 });
        Console.WriteLine(result);
    }
}

partial class Solution
{
    public int Jump(int[] nums)
    {
        int length = nums.Length;

        int highJump = 0;
        int jump = 0;
        int currPos = 0;
        if (length == 1)
        {
            return 0;
        }

        if (nums[0] == 0)
        {
            return -1;
        }

        for (int i = 0; i < length; i++)
        {
            highJump = Math.Max(highJump, i + nums[i]);
            if (highJump >= length - 1)
            {
                return jump + 1;
            }

            if (i == currPos)
            {
                currPos = highJump;
                jump++;
            }
        }

        return -1;
    }
}