using System;
using System.Collections;
using System.Text;

namespace GeekBrainsTests
{
    class MyListEnumerator : IEnumerator
    {
        Node firstNode;
        Node index;

        public MyListEnumerator(Node startNode)
        {
            firstNode = startNode;
            index = firstNode;
        }

        object IEnumerator.Current => index;

        bool IEnumerator.MoveNext()
        {
            if (index.NextNode is null) return false;
            index = index.NextNode;
            return true;
        }

        void IEnumerator.Reset()
        {
            index = firstNode;
        }
    }
}
