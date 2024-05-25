namespace AlgorithmTasks._51_75;

public class InsertInterval
{
    public void Execute()
    {
        var intervals = new int[][]
        {
            new[] { 1, 3 },
            new[] { 6, 9 }
        };
        var newInterval = new[] { 2, 5 };
        var sol = new Solution();
        var result = sol.Insert(intervals, newInterval);
        
        Console.Write("[");
        foreach (var line in result)
        {
            Console.Write("["+string.Join(", ", line)+"]");
        }
        Console.WriteLine("]");
    }
}

partial class Solution {
    public int[][] Insert(int[][] intervals, int[] newInterval)
    {
        return new[] { new[] {1} };
    }
}