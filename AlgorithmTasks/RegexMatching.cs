using System.Text.RegularExpressions;
namespace AlgorithmTasks;

public class RegexMatching
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.IsMatch("aa", "a*");
        Console.WriteLine(result);
    }
}

partial class Solution {
    public bool IsMatch(string s, string p) {
        return Regex.IsMatch(s, "^"+p+"$");
    }
}