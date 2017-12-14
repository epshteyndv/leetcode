using System;
using NUnit.Framework;

namespace Problems.LongestPalindromicSubstring
{
    public class Solution
    {
        public string LongestPalindrome(string s)
        {
            var palindromeIndexes = Tuple.Create(0, 0);

            for (var i = 0; i < s.Length - 1; i++)
            {
                var palindrome = FindPalindrome(s, Tuple.Create(i, i));

                if (palindrome.Item2 - palindrome.Item1 > palindromeIndexes.Item2 - palindromeIndexes.Item1)
                    palindromeIndexes = palindrome;
            }

            for (var i = 0; i < s.Length - 1; i++)
            {
                if (s[i] != s[i + 1])
                    continue;

                var palindrome = FindPalindrome(s, Tuple.Create(i, i + 1));

                if (palindrome.Item2 - palindrome.Item1 > palindromeIndexes.Item2 - palindromeIndexes.Item1)
                    palindromeIndexes = palindrome;
            }

            return s.Substring(palindromeIndexes.Item1, palindromeIndexes.Item2 - palindromeIndexes.Item1 + 1);
        }

        private Tuple<int,int> FindPalindrome(string s, Tuple<int, int> center)
        {
            var start = center.Item1;
            var end = center.Item2;

            while (start - 1 >= 0 && end + 1 < s.Length && s[start - 1] == s[end + 1])
            {
                start--;
                end++;
            }

            return Tuple.Create(start, end);
        }
    }

    [TestFixture]
    public class Tests
    {
        [TestCase("abccba", ExpectedResult = "abccba")]
        [TestCase("babad", ExpectedResult = "bab")]
        [TestCase("cbbd", ExpectedResult = "bb")]
        [TestCase("ccc", ExpectedResult = "ccc")]
        public string Test(string input)
        {
            return new Solution().LongestPalindrome(input);
        }
    }
}
