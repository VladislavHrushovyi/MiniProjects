namespace AlgorithmTasks._51_75;

public class PlusOne
{
    public void Execute()
    {
        var number = new int[] { 1, 2, 3 };
        var sol = new Solution();
        var result = sol.PlusOne(number);
        Console.WriteLine("[" + string.Join(", ", result) + "]");
    }
}

partial class Solution {
    public int[] PlusOne(int[] digits)
    {
        return new[] { 1 };
    }
}