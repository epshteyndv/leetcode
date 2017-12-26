using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Problems.RegularExpressionMatching
{
    public class Solution
    {
        public bool IsMatch(string s, string p)
        {
            var processes = new Queue<IProcess>();

            char? prevChar = null;

            foreach (var v in p)
            {
                if (v == '*')
                {
                    if (prevChar == null)
                        throw new Exception("Invalid regex.");

                    processes.Enqueue(new WaitZeroOrMoreChar(prevChar.Value));
                    prevChar = null;
                    continue;
                }

                if (prevChar.HasValue)
                    processes.Enqueue(new WaitChar(prevChar.Value));

                prevChar = v;
            }

            if (prevChar.HasValue)
                processes.Enqueue(new WaitChar(prevChar.Value));


            int i = 0;

            while (processes.Count > 0)
            {
                var c = i >= s.Length ? (char?) null : s[i];

                if(!ProcessChar(processes, c))
                    return false;

                i++;
            }

            return true;
        }

        private static bool ProcessChar(Queue<IProcess> processes, char? c)
        {
            var isProcessed = false;

            while (!isProcessed)
            {
                var process = processes.Peek();
                var results = process.Process(c);

                switch (results)
                {
                    case ProcessResults.Fail:
                        return false;

                    case ProcessResults.GoNextChar:
                        processes.Dequeue();
                        isProcessed = true;
                        break;

                    case ProcessResults.Nothing:
                        isProcessed = true;
                        break;

                    case ProcessResults.GoNextProcess:
                        processes.Dequeue();
                        break;
                }
            }

            return true;
        }
    }

    enum ProcessResults
    {
        GoNextChar,
        GoNextProcess,
        Fail,
        Nothing
    }

    interface IProcess
    {
        ProcessResults Process(char? c);
    }

    class WaitChar : IProcess
    {
        private readonly char _expectedChar;

        public WaitChar(char expectedChar)
        {
            _expectedChar = expectedChar;
        }

        public ProcessResults Process(char? c)
        {
            return B(c) ? ProcessResults.GoNextChar : ProcessResults.Fail;
        }

        private bool B(char? c)
        {
            if (_expectedChar == '.')
                return true;

            return c == _expectedChar;
        }
    }

    class WaitZeroOrMoreChar : IProcess
    {
        private readonly char _expectedChar;

        public WaitZeroOrMoreChar(char expectedChar)
        {
            _expectedChar = expectedChar;
        }
        
        public ProcessResults Process(char? c)
        {
            return B(c) ? ProcessResults.Nothing : ProcessResults.GoNextProcess;
        }

        private bool B(char? c)
        {
            if (_expectedChar == '.')
                return true;

            return c == _expectedChar;
        }
    }

    [TestFixture]
    public class Tests
    {
        [TestCase("a", "ab*c", ExpectedResult = false)]
        [TestCase("aa", "ab*c", ExpectedResult = false)]
        [TestCase("abc", "ab*c", ExpectedResult = true)]
        [TestCase("ac", "ab*c", ExpectedResult = true)]
        [TestCase("abbbc", "ab*c", ExpectedResult = true)]
        [TestCase("acb", "ab*c", ExpectedResult = false)]
        [TestCase("aa", "aa", ExpectedResult = true)]
        [TestCase("aaa", "aa", ExpectedResult = false)]
        [TestCase("aa", "a*", ExpectedResult = true)]
        [TestCase("aa", ".*", ExpectedResult = true)]
        [TestCase("ab", ".*", ExpectedResult = true)]
        [TestCase("aab", "c*a*b", ExpectedResult = true)]
        public bool Test(string str, string pattern)
        {
            return new Solution().IsMatch(str, pattern);
        }
    }
}
