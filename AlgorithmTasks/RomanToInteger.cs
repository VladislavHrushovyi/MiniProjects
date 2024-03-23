namespace AlgorithmTasks;

public class RomanToInteger
{
    public void Execute()
    {
        var sol = new Solution();
        var res1 = sol.RomanToInt("III");
        var res2 = sol.RomanToInt("LVIII");
        var res3 = sol.RomanToInt("MCMXCIV");

        Console.WriteLine(res1);
        Console.WriteLine(res2);
        Console.WriteLine(res3);
    }
}

partial class Solution
{
    private readonly Dictionary<char, int> _romansValue = new()
    {
        { 'M', 1000 },
        { 'D', 500 },
        { 'C', 100 },
        { 'L', 50 },
        { 'X', 10 },
        { 'V', 5 },
        { 'I', 1 },
    };

    public int RomanToInt(string s)
    {
        int result = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (i == s.Length - 1)
            {
                result += _romansValue[s[i]];
                break;
            }

            if (_romansValue[s[i]] >= _romansValue[s[i + 1]])
            {
                result += _romansValue[s[i]];
            }
            else
            {
                result += _romansValue[s[i + 1]] - _romansValue[s[i]];
                i++;
            }
        }

        return result;
    }
}