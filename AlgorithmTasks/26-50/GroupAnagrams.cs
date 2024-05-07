namespace AlgorithmTasks._26_50;

public class GroupAnagrams
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.GroupAnagrams(new[] { "eat","tea","tan","ate","nat","bat" });
        foreach (var line in result)
        {
            Console.WriteLine("(" + string.Join(", ", line) + ")");
        }
    }
}

partial class Solution {
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        IList<IList<string>> results = new List<IList<string>>();

        return results;
    }
}