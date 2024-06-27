namespace AlgorithmTasks._51_75;

public class ClimbingStairs
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.ClimbStairs(2);
        Console.WriteLine(result);
    }
}

partial class Solution {
    public int ClimbStairs(int n)
    {
        int[] tab = new int[n + 1];
        if (tab.Length > 0) tab[0] = 1;
        if (tab.Length > 1) tab[1] = 1;
        for (int i = 2; i < tab.Length; i++) 
            tab[i] = tab[i - 1] + tab[i - 2];
        return tab[n];
    }
}