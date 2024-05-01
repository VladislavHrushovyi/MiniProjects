using System.Text;

namespace AlgorithmTasks._26_50;

public class Permutations
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.Permute(new[] { 1, 2, 3 });
        var sb = new StringBuilder();
        foreach (var row in result)
        {
            sb.Append($"[{string.Join(",", row)}] ");
        }

        Console.WriteLine(sb);
    }
}

partial class Solution
{
    public IList<IList<int>> Permute(int[] nums)
    {
        var result = new List<IList<int>>();
        var queue = new Queue<List<int>>();
        queue.Enqueue(new List<int>());

        for (int i = 0; i < nums.Length; i++)
        {
            int size = queue.Count;
            for (int j = 0; j < size; j++)
            {
                var curr = queue.Dequeue();
                for (int k = 0; k <= curr.Count; k++)
                {
                    var temp = new List<int>(curr);
                    temp.Insert(k, nums[i]);
                    queue.Enqueue(temp);
                }
            }
        }

        while (queue.Count > 0)
            result.Add(queue.Dequeue());

        return result;
    }
}