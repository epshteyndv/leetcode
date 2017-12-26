using System.Collections.Generic;
using NUnit.Framework;

namespace Problems.ReverseNodesInKGroup
{
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int x)
        {
            val = x;
        }
    }

    public class Solution
    {
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (k == 1)
                return head;

            var fakeRoot = new ListNode(default(int)) {next = head};

            var groupStart = fakeRoot;
            while (true)
            {
                var range = groupStart.GetRange(k);

                if (range == null)
                    break;

                range.Revert();
                groupStart = range.end;
            }
            
            return fakeRoot.next;
        }
    }

    public class ListNodeRange
    {
        public ListNode root;
        public ListNode end;
    }

    public static class ListNodeExtensions
    {
        public static ListNodeRange GetRange(this ListNode root, int size)
        {
            var i = 0;
            var end = root;

            while (end != null && i++ != size)
                end = end.next;

            return end == null ? null : new ListNodeRange {root = root, end = end};
        }

        public static void Revert(this ListNodeRange range)
        {
            var rangeEnd = range.root.next;

            while (range.root.next != range.end)
                range.end.InsertChild(range.root.RemoveChild());

            range.end = rangeEnd;
        }

        private static void InsertChild(this ListNode root, ListNode item)
        {
            item.next = root.next;
            root.next = item;
        }

        private static ListNode RemoveChild(this ListNode root)
        {
            var child = root.next;
            root.next = child.next;
            child.next = null;

            return child;
        }
    }

    [TestFixture]
    public class Tests
    {
        [TestCase(new[] {1, 2, 3}, 3, ExpectedResult = new[] {3, 2, 1})]
        [TestCase(new[] {1, 2, 3, 4, 5}, 2, ExpectedResult = new[] {2, 1, 4, 3, 5})]
        [TestCase(new[] {1, 2, 3, 4, 5}, 3, ExpectedResult = new[] {3, 2, 1, 4, 5})]
        [TestCase(new[] {1, 2, 3, 4, 5}, 4, ExpectedResult = new[] {4, 3, 2, 1, 5})]
        public int[] Test(int[] input, int k)
        {
            return ToArray(new Solution().ReverseKGroup(ToListNode(input), k));
        }
        
        // [5, 4, 3] = 3->4->5
        private static ListNode ToListNode(int[] numbers)
        {
            ListNode Node(int index)
            {
                var listNode = new ListNode(numbers[index]);

                if (index + 1 < numbers.Length)
                    listNode.next = Node(index + 1);

                return listNode;
            }

            return Node(0);
        }

        // 3->4->5 = [3, 4, 5]
        private static int[] ToArray(ListNode firstNode)
        {
            var list = new List<int>();

            void FillList(ListNode node)
            {
                list.Add(node.val);

                if (node.next != null)
                    FillList(node.next);
            }

            FillList(firstNode);

            return list.ToArray();
        }
    }
}
