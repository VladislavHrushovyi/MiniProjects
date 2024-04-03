namespace AlgorithmTasks._26_50;

public class FirstEntryIndexStrInStr
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.StrStr("abc", "c");
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int StrStr(string haystack, string needle)
    {
        if (string.IsNullOrEmpty(haystack) || needle.Length > haystack.Length)
        {
            return -1;
        }

        if (haystack == needle)
        {
            return 0;
        }

        for (int i = 0; i < haystack.Length; i++)
        {
            if (IsSubString(i, haystack, needle))
            {
                return i;
            }
        }
        return -1;
    }

    private bool IsSubString(int startIndex, string haystack, string needle)
    {
        if (needle.Length + startIndex> haystack.Length)
        {
            return false;
        }
        for (int i = 0; i < needle.Length; i++)
        {
            if (haystack[startIndex + i] != needle[i])
            {
                return false;
            }
        }

        return true;
    }
}