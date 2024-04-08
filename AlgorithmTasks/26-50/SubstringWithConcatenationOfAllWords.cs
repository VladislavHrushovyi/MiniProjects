namespace AlgorithmTasks._26_50;

public class SubstringWithConcatenationOfAllWords
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.FindSubstring("barfoothefoobarman",
            new[] {"foo","bar"});
        Console.WriteLine($"{string.Join(',', result)}");
    }
}

partial class Solution {
    public IList<int> FindSubstring(string s, string[] words)
    {
        IList<int> result = new List<int>();
        if (string.IsNullOrEmpty(s) || words.Length == 0)
        {
            return result;
        }

        int wordLength = words[0].Length;
        int substrLength = wordLength * words.Length;
        if (substrLength > s.Length)
        {
            return result;
        }
        Dictionary<string, int> wordDict = words.GroupBy(s => s)
            .ToDictionary(k => k.Key, v => v.Count());
        for (int i = 0; i <= s.Length - substrLength; i+= 1)
        {
            var currSubStr = s.Substring(i, substrLength);
            if (IsValidSubStr(currSubStr, wordDict, wordLength))
            {
                result.Add(i);
            }
        }
        return result;
    }

    private bool IsValidSubStr(string currSubStr, Dictionary<string, int> wordDict, int wordLength)
    {
        int num = currSubStr.Length / wordLength;
        bool isValid = true;
        Dictionary<string, int> newWordDict = new(wordDict);

        for (int i = 0; i < num; i++)
        {
            int index = i * wordLength;
            string wordFromString = currSubStr.Substring(index, wordLength);
            if (!newWordDict.ContainsKey(wordFromString))
            {
                isValid = false;
                break;
            }

            if (newWordDict[wordFromString] > 1)
            {
                newWordDict[wordFromString]--;
            }
            else
            {
                newWordDict.Remove(wordFromString);
            }
        }

        return isValid;
    }
}