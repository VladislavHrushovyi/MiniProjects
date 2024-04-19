namespace AlgorithmTasks._26_50;

public class CombinationSum
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.CombinationSum(new[] { 2, 3, 6, 7 }, 7);
        foreach (var line in result)
        {
            Console.WriteLine($"{string.Join(",", line)}");
        }
    }
}

partial class Solution {
    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        IList <IList<int>> result = new List<IList<int>>();
        
        Array.Sort(candidates);
        BackTrackCombineSum(candidates, target, 0, new List<int>(), result);

        return result;
    }

    private void BackTrackCombineSum(int[] candidates, int target, int i, List<int> temps, IList<IList<int>> result)
    {
        if (target == 0)
        {
            result.Add(temps.ToList());
            return;
        }

        while (i < candidates.Length)
        {
            if (candidates[i] > target)
            {
                break;
            }
            
            temps.Add(candidates[i]);
            BackTrackCombineSum(candidates, target - candidates[i], i, temps, result);
            temps.RemoveAt(temps.Count - 1);
            i++;
        }
    }
}