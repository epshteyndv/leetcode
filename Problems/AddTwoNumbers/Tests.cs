using System.Collections.Generic;
using NUnit.Framework;

namespace Problems.AddTwoNumbers
{
    [TestFixture]
    internal class Tests
    {
        [TestCase(new[] { 0 }, new[] { 0 }, ExpectedResult = new[] { 0 })]
        [TestCase(new[] { 1, 0, 0 }, new[] { 1, 0, 0 }, ExpectedResult = new[] {2, 0 ,0 })]
        [TestCase(new[] { 1 }, new[] { 1 }, ExpectedResult = new[] { 2 })]
        [TestCase(new[] { 1, 2, 3 }, new[] { 3, 4, 5 }, ExpectedResult = new[] { 4, 6, 8 })]
        [TestCase(new[] { 8, 1, 1 }, new[] { 5, 5, 5 }, ExpectedResult = new[] { 1, 3, 6, 6 })]
        [TestCase(new[] { 1, 1, 8 }, new[] { 5, 5, 5 }, ExpectedResult = new[] { 6, 7, 3 })]
        public int[] Test(int[] firstNumber, int[] secondNumber)
        {
            return ToArray(new Solution().AddTwoNumbers(ToListNode(firstNumber), ToListNode(secondNumber)));
        }

        // [5, 4, 3] = 3->4->5
        private static ListNode ToListNode(int[] numbers)
        {
            ListNode Node(int index)
            {
                var listNode = new ListNode(numbers[index]);

                if (index - 1 >= 0)
                    listNode.next = Node(index - 1);

                return listNode;
            }

            return Node(numbers.Length - 1);
        }

        // 3->4->5 = [5, 4, 3]
        private static int[] ToArray(ListNode firstNode)
        {
            var list = new List<int>();

            void FillList(ListNode node)
            {
                if (node.next != null)
                    FillList(node.next);

                list.Add(node.val);
            }

            FillList(firstNode);

            return list.ToArray();
        }
    }
}
