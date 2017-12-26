using System;
using NUnit.Framework;

namespace Problems.JumpGameII
{
    public class Solution
    {
        public int Jump(int[] nums)
        {
            var v = new int[nums.Length];

            for (var i = 1; i < v.Length; i++)
                v[i] = int.MaxValue;

            for (var i = 0; i < nums.Length; i++)
            for (var j = Math.Min(v.Length - 1, i + nums[i]); j > i; j--)
            {
                v[j] = Math.Min(v[i] + 1, v[j]);

                if (j == v.Length - 1)
                    return v[v.Length - 1];
            }

            return int.MaxValue;
        }
    }

    public class Tests
    {
        [TestCase(new[] {3, 2, 1}, ExpectedResult = 1)]
        [TestCase(new[] {1, 1, 1, 1}, ExpectedResult = 3)]
        public int Test(int[] nums)
        {
            return new Solution().Jump(nums);
        }
    }
}
