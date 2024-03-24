namespace AlgorithmTasks;

public class LongestPrefix
{
    public void Execute()
    {
        var sol = new Solution();
        var res = sol.LongestCommonPrefix(new[] { "flower", "flow", "flight" });
        Console.WriteLine(res);
    }
}

partial class Solution {
    public string LongestCommonPrefix(string[] strs)
    {
        int minLength = strs.Min(x => x.Length);
        string commonPrefix = "";
        for (int i = 0; i < minLength; i++)
        {
            char commonChar = strs[0][i];
            for (int j = 1; j < strs.Length; j++)
            {
                if (strs[j][i] != commonChar)
                {
                    return commonPrefix;
                }
            }

            commonPrefix += commonChar;
        }

        return commonPrefix;
    }
}

