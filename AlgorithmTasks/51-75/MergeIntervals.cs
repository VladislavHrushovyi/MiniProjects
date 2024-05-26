namespace AlgorithmTasks._51_75;

public class MergeIntervals
{
    public void Execute()
    {
        var data = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 2, 6 },
            new int[] { 8, 10 },
            new int[] { 15, 18 },
        };

        var sol = new Solution();
        var result = sol.Merge(data);

        Console.Write("[");
        foreach (var line in result)
        {
            Console.Write("[" + string.Join(",", line) + "]");
        }

        Console.WriteLine("]");
    }
}

partial class Solution
{
    public int[][] Merge(int[][] intervals)
    {
        List<int[]> output = new List<int[]>();

        Array.Sort(intervals, (a, b) => a[0] - b[0]);

        output.Add(intervals[0]);

        for (int i = 1; i < intervals.Length; i++)
        {
            if (output[^1][1] >= intervals[i][0])
            {
                if (output[^1][1] <= intervals[i][1])
                    output[^1][1] = intervals[i][1];
            }
            else output.Add(intervals[i]);
        }

        return output.ToArray();
    }
}