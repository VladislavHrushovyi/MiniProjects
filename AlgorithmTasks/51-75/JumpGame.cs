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
        return true;
    }
}