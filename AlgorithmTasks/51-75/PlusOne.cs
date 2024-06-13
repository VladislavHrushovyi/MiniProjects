namespace AlgorithmTasks._51_75;

public class PlusOne
{
    public void Execute()
    {
        var number = new int[] { 1, 2, 3 };
        var sol = new Solution();
        var result = sol.PlusOne(number);
        Console.WriteLine("[" + string.Join(", ", result) + "]");
    }
}

partial class Solution {
    public int[] PlusOne(int[] digits)
    {
        int index = 1;
        return StartSummary(digits, index).ToArray();
    }

    int[] StartSummary(int[] digits, int index)
    {
        digits[^index]++;
        if (digits[^index] != 10) return digits;
        
        digits[^index] = 0;
        index++;
        if (digits.Length >= index) return StartSummary(digits, index);
        
        var result = new int[digits.Length + 1];
        result[0] = 1;
        return result;
    }
}