namespace AlgorithmTasks._26_50;

public class SubstringWithConcatenationOfAllWords
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.FindSubstring("wordgoodgoodgoodbestword",
            new[] {"word","good","best","good"});
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

        int wordlength = words[0].Length;
        int substrLength = wordlength * words.Length;
        if (substrLength > s.Length)
        {
            return result;
        }

        for (int i = 0; i < s.Length - substrLength + 1; i+= 1)
        {
            var currSubStr = s.Substring(i, substrLength);
            foreach (var word in words)
            {
                currSubStr = ReplaceFirst(currSubStr, word, "");
                if (string.IsNullOrEmpty(currSubStr))
                {
                    result.Add(i);
                }
            }
        }
        return result;
    }

    private string ReplaceFirst(string originalText, string search, string replace)
    {
        int index = originalText.IndexOf(search, StringComparison.Ordinal);
        if (index == -1)
        {
            return originalText;
        }

        return originalText.Substring(0, index) + replace + originalText.Substring(index + search.Length);
    }
}