namespace AlgorithmTasks._26_50;

public class RemoveElement
{
    public void Execute()
    {
        var sol = new Solution();
        var items = new[] { 1, 2, 3, 4, 4, 4, 5, 6, 6, 6, 6 };
        var result = sol.RemoveElement(items, 4);
        Console.WriteLine(string.Join(',', items.Take(result)));
    }
}

partial class Solution {
    public int RemoveElement(int[] nums, int val)
    {
        if (nums.Length == 0)
        {
            return 0;
        }
        int counter = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != val)
            {
                nums[counter] = nums[i];
                counter++;
            }
        }
        return counter;
    }
}