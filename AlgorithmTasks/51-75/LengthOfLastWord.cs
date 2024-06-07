namespace AlgorithmTasks._51_75;

public class LengthOfLastWord
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.LengthOfLastWord("Hello World");
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int LengthOfLastWord(string s)
    {
        s = s.Trim();
        
        int length = 0;
        for (int i = s.Length - 1; i >= 0; i--) {
            if (s[i] != ' ') {
                length++;
            }
            else if (length > 0) {
                break;
            }
        }
        
        return length;
    }
}