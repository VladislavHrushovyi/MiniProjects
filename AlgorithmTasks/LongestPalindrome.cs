namespace AlgorithmTasks;

public class LongestPalindrome
{
    public void Execute()
    {
        var solution = new Solution();
        var result = solution.LongestPalindrome("aaccdd");
        Console.WriteLine(result);
    }
}

partial class Solution {
    public string LongestPalindrome(string s)
    {
        if (s.Length == 1)
        {
            return s;
        }

        int start = 0;
        int resultLengts = 0;

        for (int i = 0; i < s.Length; i++)
        {
            int left = i;
            int right = i;

            while (left >= 0 && right < s.Length && s[left] == s[right])
            {
                if (right - left + 1 > resultLengts)
                {
                    start = left;
                    resultLengts = right - left + 1;
                }

                left--;
                right++;
            }

            left = i;
            right = i + 1;

            while (left >= 0 && right < s.Length && s[left] == s[right])
            {
                if (right - left + 1 > resultLengts)
                {
                    start = left;
                    resultLengts = right - left + 1;
                }

                left--;
                right++;
            }
        }

        return s.Substring(start, resultLengts);
    }
}