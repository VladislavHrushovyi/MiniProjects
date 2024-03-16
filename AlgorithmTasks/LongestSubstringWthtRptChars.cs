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
        if (s.Length < 2)
        {
            return s.Length;
        }

        int maxLength = 0;
        int k = 0;
        int count = 0;

        for (int i = 1; i < s.Length; i++)
        {
            for (int j = k; j < i; j++)
            {
                if (s[i] == s[j])
                {
                    k = j + 1;
                }
            }

            count = i - k + 1;
            if (count > maxLength)
            {
                maxLength = count;
            }
        }

        return maxLength;
    }
}