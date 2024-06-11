namespace AlgorithmTasks._51_75;

public class ValidNumber
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.IsNumber("0");
        Console.WriteLine(result);
    }
}

partial class Solution {
    public bool ReadInteger(string s, ref int index)
    {
        int i = index;
        if (i < s.Length && (s[i] == '-' || s[i] == '+'))
            ++i;
        
        bool hasDigits = false;
        while (i < s.Length && Char.IsDigit(s[i]))
        {
            ++i;
            hasDigits = true;
        }

        if (hasDigits && (i == s.Length || s[i] != '.'))
        {
            index = i;
            return true;
        }

        return false;
    }

    public bool ReadDecimal(string s, ref int index)
    {
        int i = index;
        if (i < s.Length && (s[i] == '-' || s[i] == '+'))
            ++i;
        
        bool hasLeftDigits = false;
        while (i < s.Length && Char.IsDigit(s[i]))
        {
            ++i;
            hasLeftDigits = true;
        }

        if (i == s.Length || s[i] != '.')
            return false;

        ++i;
        bool hasRightDigits = false;
        while (i < s.Length && Char.IsDigit(s[i]))
        {
            ++i;
            hasRightDigits = true;
        }

        if (hasLeftDigits || hasRightDigits)
        {
            index = i;
            return true;
        }

        return false;
    }

    public bool IsNumber(string s) 
    {
        int i = 0;
        if (ReadInteger(s, ref i) || ReadDecimal(s, ref i))
        {
            if (i == s.Length)
                return true;
            if (Char.ToLower(s[i]) != 'e')
                return false;
            ++i;
            if (ReadInteger(s, ref i) && i == s.Length)
                return true;
        }

        return false;
    }
}