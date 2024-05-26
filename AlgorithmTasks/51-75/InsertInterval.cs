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
        List<int[]> result = new List<int[]>();
        int i = 0;
        
        while (i < intervals.Length && intervals[i][1] < newInterval[0]) {
            result.Add(intervals[i]);
            i++;
        }
        
        while (i < intervals.Length && intervals[i][0] <= newInterval[1]) {
            newInterval[0] = Math.Min(newInterval[0], intervals[i][0]);
            newInterval[1] = Math.Max(newInterval[1], intervals[i][1]);
            i++;
        }
        result.Add(newInterval);
        
        while (i < intervals.Length) {
            result.Add(intervals[i]);
            i++;
        }
        
        return result.ToArray();
    }
}