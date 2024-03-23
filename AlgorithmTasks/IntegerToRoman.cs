namespace AlgorithmTasks;

public class IntegerToRoman
{
    public void Execute()
    {
        var sol = new Solution();
        var res1 = sol.IntToRoman(3);
        var res2 = sol.IntToRoman(58);
        var res3 = sol.IntToRoman(1994);
        
        Console.WriteLine(res1);
        Console.WriteLine(res2);
        Console.WriteLine(res3);
    }
}

partial class Solution {
    public string IntToRoman(int num)
    {
        return num switch
        {
            >= 1000 => "M" + IntToRoman(num - 1000),
            >= 900 => "CM" + IntToRoman(num - 900),
            >= 500 => "D" + IntToRoman(num - 500),
            >= 400 => "CD" + IntToRoman(num - 400),
            >= 100 => "C" + IntToRoman(num - 100),
            >= 90 => "XC" + IntToRoman(num - 90),
            >= 50 => "L" + IntToRoman(num - 50),
            >= 40 => "XL" + IntToRoman(num - 40),
            >= 10 => "X" + IntToRoman(num - 10),
            >= 9 => "IX" + IntToRoman(num - 9),
            >= 5 => "V" + IntToRoman(num - 5),
            >= 4 => "IV" + IntToRoman(num - 4),
            >= 1 => "I" + IntToRoman(num - 1),
            _ => ""
        };
    }
}