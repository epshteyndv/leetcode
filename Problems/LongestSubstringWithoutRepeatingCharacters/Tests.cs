using NUnit.Framework;

namespace Problems.LongestSubstringWithoutRepeatingCharacters
{
    [TestFixture]
    public class Tests
    {
        [TestCase("", ExpectedResult = 0)]
        [TestCase("aaa", ExpectedResult = 1)]
        [TestCase("ababab", ExpectedResult = 2)]
        [TestCase("1212abcdef", ExpectedResult = 8)]
        [TestCase("1212abcdef123123", ExpectedResult = 9)]
        public int Test(string str)
        {
            return new Solution().LengthOfLongestSubstring(str);
        }
    }
}