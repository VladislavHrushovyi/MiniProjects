namespace AlgorithmTasks._51_75;

public class MergeIntervals
{
    public void Execute()
    {
        var data = new int[][]
        {
            new int[]{1,3},
            new int[]{2,6},
            new int[]{8,10},
            new int[]{15,18},
        };
        
        var sol = new Solution();
        var result = sol.Merge(data);

        Console.Write("[");
        foreach (var line in result)
        {
            Console.Write("["+string.Join(",", line)+"]");
        }
        Console.WriteLine("]");
    }
}

partial class Solution {
    public int[][] Merge(int[][] intervals)
    {
        return new[] { new[] {1} };
    }
}