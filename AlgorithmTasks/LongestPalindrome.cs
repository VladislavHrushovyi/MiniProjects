namespace AlgorithmTasks;

public class LongestPalindrome
{
    public void Execute()
    {
        var solution = new Solution();
        var result = solution.LongestPalindrome("babad");
        Console.WriteLine(result);
    }
}

public partial class Solution {
    public string LongestPalindrome(string s)
    {
        if (s.Length == 1)
        {
            return s;
        }

        if (s.Length == 2)
        {
            if (s[0] != s[1])
            {
                return $"{s[0]}";   
            }
        }

        if (s.ToCharArray().ToHashSet().Count == 1)
        {
            return s;
        }
        var result = string.Empty;
        for (int i = 0; i < s.Length; i++)
        {
            var leftChar = s[i];
            var leftIndex = i;

            if (leftIndex + 1 > s.Length - 1)
            {
                continue;
            }
            var withoutLeft = s.Substring(i + 1);
            while (withoutLeft.Contains(leftChar))
            {
                var rightIndex = withoutLeft.IndexOf(leftChar) + s.Length - withoutLeft.Length;
                var tempLeftIndex = leftIndex;
                var tempRightIndex = rightIndex;
                var isPolindrome = true;
                while (tempRightIndex - tempLeftIndex != 1)
                {
                    if (s[tempLeftIndex] == s[tempRightIndex])
                    {
                        tempLeftIndex++;
                        tempRightIndex--;
                        if (tempRightIndex - tempLeftIndex == 0)
                        {
                            isPolindrome = true;
                            break;
                        }
                    }
                    else
                    {
                        isPolindrome = false;
                        break;
                    }
                }

                if (isPolindrome)
                {
                    var newSubstringPalindrome = s.Substring(leftIndex, rightIndex - leftIndex + 1);

                    result = newSubstringPalindrome.Length >= result.Length ? newSubstringPalindrome : result;
                }

                if (rightIndex <= withoutLeft.Length)
                {
                    withoutLeft = withoutLeft.Substring(rightIndex);
                }
                else
                {
                    withoutLeft = String.Empty;
                }
            }
        }

        return result;
    }
}