using System;
using System.Linq;
using NUnit.Framework;

namespace Problems.TrappingRainWater
{
    public class Solution
    {
        public int Trap(int[] height)
        {
            var level = new int[height.Length];

            var leftMax = 0;
            for (var i = 0; i < height.Length; i++)
            {
                leftMax = Math.Max(height[i], leftMax);
                level[i] = leftMax - height[i];
            }
            
            var rightMax = 0;
            for (var i = height.Length - 1; i >= 0; i--)
            {
                rightMax = Math.Max(height[i], rightMax);

                if (rightMax >= leftMax)
                    break;

                level[i] = rightMax - height[i];
            }

            return level.Sum();
        }
    }

    [TestFixture]
    public class Tests
    {
        [TestCase(new[] {0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1}, ExpectedResult = 6)]
        public int Test(int[] height)
        {
            return new Solution().Trap(height);
        }
    }
}
