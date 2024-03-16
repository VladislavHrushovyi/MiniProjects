namespace AlgorithmTasks;

public class LongestSubstringWthtRptChars
{
    public void Execute()
    {
        var solution = new Solution();

        var str = "jbpnbwwd";
        int result =  solution.LengthOfLongestSubstring(str);
        Console.WriteLine(result);
    }
}
partial class Solution 
{
    public int LengthOfLongestSubstring(string s)
    {
        if (s == "")
        {
            return 0;
        }
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s) || s.Length == 1)
        {
            return 1;
        }
        int maxSubstr = 0;
        var subItems = new List<char>();
        for (int i = 0; i < s.Length; i++)
        {
            for (int j = i; j < s.Length; j++)
            {
                if (subItems.Contains(s[j]))
                {
                    maxSubstr = subItems.Count > maxSubstr ? subItems.Count : maxSubstr;
                    subItems.Clear();
                    break;
                }
                subItems.Add(s[j]);
            }
        }
        return maxSubstr;
    }
}