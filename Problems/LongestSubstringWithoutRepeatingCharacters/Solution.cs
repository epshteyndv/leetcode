using System;
using System.Collections.Generic;

namespace Problems.LongestSubstringWithoutRepeatingCharacters
{
    public class Solution
    {
        public int LengthOfLongestSubstring(string s)
        {
            var max = 0;

            for (var i = 0; i < s.Length; i++)
            {
                max = Math.Max(max, LengthOfLongestSubstringFromIndex(s, i));

                if (max == s.Length - i)
                    return max;
            }

            return max;
        }

        private int LengthOfLongestSubstringFromIndex(string s, int i)
        {
            var existingChars = new HashSet<char> {s[i]};
            var j = i + 1;

            while (j < s.Length && !existingChars.Contains(s[j]))
                existingChars.Add(s[j++]);

            return existingChars.Count;
        }
    }
}
