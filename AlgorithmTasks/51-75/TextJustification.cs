namespace AlgorithmTasks._51_75;

public class TextJustification
{
    public void Execute()
    {
        var data = new string[] { "This", "is", "an", "example", "of", "text", "justification." };
        var sol = new Solution();
        var result = sol.FullJustify(data, 16);
        Console.WriteLine(string.Join(",\n", result));
    }
}

partial class Solution {
    public IList<string> FullJustify(string[] words, int maxWidth)
    {
        var res = new List<string>();
        var cur = new List<string>();
        int numOfLetters = 0;

        foreach (var word in words) {
            if (word.Length + cur.Count + numOfLetters > maxWidth) {
                for (int i = 0; i < maxWidth - numOfLetters; i++) {
                    cur[i % (cur.Count - 1 > 0 ? cur.Count - 1 : 1)] += " ";
                }
                res.Add(string.Join("", cur));
                cur.Clear();
                numOfLetters = 0;
            }
            cur.Add(word);
            numOfLetters += word.Length;
        }

        string lastLine = string.Join(" ", cur);
        while (lastLine.Length < maxWidth) lastLine += " ";
        res.Add(lastLine);

        return res;
    }
}