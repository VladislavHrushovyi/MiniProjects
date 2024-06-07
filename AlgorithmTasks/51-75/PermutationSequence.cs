using System.Text;

namespace AlgorithmTasks._51_75;

public class PermutationSequence
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.GetPermutation(3, 3);
        Console.WriteLine(result);
    }
}

partial class Solution {
    public string GetPermutation(int n, int k) {
        var factorial = 1;
        var res = new StringBuilder();

        for(int i=1;i<n;i++){
            factorial *= i;
        }

        var list = new List<int>();
        for(int i=1;i<=n;i++){
            list.Add(i);
        }
        k-=1;

        while(true){
            int index = k/factorial;
            res.Append(list.ElementAt(index));
            list.RemoveAt(index);
            
            if(list.Count==0)
                break;

            k %= factorial;
            factorial /= list.Count; 
        }
        return res.ToString();
    }
}