using System.Text;

namespace AlgorithmTasks._26_50;

public class CountAndSay
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.CountAndSay(4);
        
        Console.WriteLine(result);
    }
}

partial class Solution {
    public string CountAndSay(int n)
    {
        if (n == 1)
        {
            return "1";
        }
        return Say(CountAndSay(n - 1));
    }

    private string Say(string s)
    {
        int stringLength = s.Length;
        StringBuilder sb = new();
        int currCount = 0;
        for (int i = 0; i < stringLength - 1; i++)
        {
            currCount++;
            if (s[i] != s[i + 1])
            {
                sb.Append(currCount.ToString());
                sb.Append(s[i]);
                currCount = 0;
            }
        }
        
        currCount++;
        sb.Append(currCount.ToString());
        sb.Append(s[stringLength - 1]);
        return sb.ToString();
    }
}