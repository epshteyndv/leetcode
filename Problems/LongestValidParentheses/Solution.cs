using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Problems.LongestValidParentheses
{
    public class Solution
    {
        private const int Open = -1;
        private const int Close = -2;

        public int LongestValidParentheses(string s)
        {
            var list = new List<int>();

            foreach (var c in s)
                MergeItems(list, c == '(' ? Open : Close);

            list.Add(0);
            return list.Max() * 2;
        }

        private void MergeItems(List<int> list, int item)
        {
            if (item == Open)
            {
                list.Add(item);
                return;
            }

            if (list.Count - 1 >= 0)
                if (list[list.Count - 1] == Open)
                    list[list.Count - 1] = 1;
                else if (list[list.Count - 1] > 0 && list.Count - 2 >= 0 && list[list.Count - 2] == Open)
                    list[list.Count - 2] = 1;
                else
                    list.Add(item);

            while (list.Count - 1 >=0 && list[list.Count - 1] > 0 && list.Count - 2 >= 0 && list[list.Count - 2] > 0)
            {
                list[list.Count - 2] += list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
            }
        }
    }

    [TestFixture]
    public class Tests
    {
        [TestCase("()(()", ExpectedResult = 2)]
        [TestCase("()(())", ExpectedResult = 6)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("(", ExpectedResult = 0)]
        [TestCase(")", ExpectedResult = 0)]
        public int Test(string s)
        {
            return new Solution().LongestValidParentheses(s);
        }
    }
}
