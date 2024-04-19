namespace AlgorithmTasks._26_50;

public class CombinationSumTwo
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.CombinationSum2(new []{10,1,2,7,6,1,5}, 8);

        foreach (var line in result)
        {
            Console.WriteLine(string.Join(",", line));
        }
    }
}

partial class Solution {
    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        IList<IList<int>> result = new List<IList<int>>();
        Array.Sort(candidates);
        BacktrackingConcretingSum(candidates, target, 0, new List<int>(), result);
        return result;
    }

    private void BacktrackingConcretingSum(int[] candidates, int target, int start, List<int> currCand, IList<IList<int>> result)
    {
        if(target < 0){
            return;
        }
        if(target == 0){
            result.Add(new List<int>(currCand));
            return;
        }
        for(int i = start; i<candidates.Length; i++){
            if(i > start && candidates[i] == candidates[i - 1]){
                continue;
            }
            currCand.Add(candidates[i]);
            BacktrackingConcretingSum(candidates, target-candidates[i], i + 1, currCand, result);
            currCand.RemoveAt(currCand.Count-1);
        }
    }
}