using System;
using System.Collections.Generic;
using System.Text;

namespace CustomStack
{
    public class StackOfStrings
    {
        private List<string> data;

        public StackOfStrings()
        {
            data = new List<string>();
        }

        public void Push(string item)
        {
            data.Add(item);
        }

        public string Pop()
        {
            var current = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);

            return current;
        }

        public string Peek()
        {
            var current = data[data.Count - 1];

            return current;
        }

        public bool IsEmpty()
        {
            return data.Count == 0;
        }
    }
}
