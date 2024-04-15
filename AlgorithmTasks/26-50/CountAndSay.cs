namespace AlgorithmTasks._26_50;

public class CountAndSay
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.CountAndSay(5);
        
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
        return String.Empty;
    }
}