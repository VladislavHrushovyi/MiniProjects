using System.Text;

namespace AlgorithmTasks;

public class LetterCombination
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.LetterCombinations("23");
        var sb = new StringBuilder();
        sb.Append($"[{string.Join(',', result)}]");
        Console.WriteLine(sb);
    }
}

partial class Solution {
    private readonly Dictionary<char, string> _keyboard = new()
    {
        {'2', "abc"},
        {'3', "def"},
        {'4', "ghi"},
        {'5', "jkl"},
        {'6', "mno"},
        {'7', "pqrs"},
        {'8', "tuv"},
        {'9', "wxyz"},
    };
    public IList<string> LetterCombinations(string digits)
    {
        var result = new List<string>();
        if (string.IsNullOrEmpty(digits)) return result;

        Combine("", digits, 0, result );
        return result;
    }

    private void Combine(string combination, string digits, int index, List<string> result)
    {
        if (index == digits.Length)
        {
            result.Add(combination);
            return;
        }

        foreach (var letter in _keyboard[digits[index]])
        {
            Combine(combination + letter, digits, index + 1, result);
        }
    }
}