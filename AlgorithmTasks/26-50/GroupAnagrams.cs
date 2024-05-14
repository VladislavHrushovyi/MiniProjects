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
        if (strs == null || strs.Length == 0) {
            return new List<IList<string>>();
        }
        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        foreach (string s in strs) {
            char[] ch = s.ToCharArray();
            Array.Sort(ch);
            string keyStr = new string(ch);

            if (!map.ContainsKey(keyStr)) {
                map[keyStr] = new List<string>();
            }
            map[keyStr].Add(s);
        }
        return new List<IList<string>>(map.Values);
    }
}