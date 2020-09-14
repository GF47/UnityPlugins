using System;
using System.Collections;
using System.Collections.Generic;

namespace GF47RunTime.Collections
{
    // todo 可遍历的队列
    public class IndexedQueue<T> : ICollection<T>, ICollection
    {

        private Node _head;
        private Node _rear;
        private int _count;
        private KeyValuePair<int, Node> _indicator;

        public IndexedQueue()
        {
            _head = new Node(default(T), this);
            _rear = _head;
            _count = 0;
        }
        ~IndexedQueue()
        {
            _rear = _head;
            _count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count) { throw new ArgumentOutOfRangeException(); }

                if (_indicator.Key < 0 || _indicator.Key > index) { _indicator = new KeyValuePair<int, Node>(0, _head.Next); }

                while (_indicator.Key < index)
                {
                    _indicator = new KeyValuePair<int, Node>(_indicator.Key + 1, _indicator.Value.Next);
                }

                return _indicator.Value.Value;
            }
        }

        public int Count { get { return _count; } }

        bool ICollection.IsSynchronized => true;

        object ICollection.SyncRoot => new object();

        bool ICollection<T>.IsReadOnly => false;

        void ICollection<T>.Add(T item) { Enqueue(item); }

        bool ICollection<T>.Remove(T item) { throw new NotImplementedException(); }

        public void Enqueue(T item)
        {
            var nn = new Node(item, this);
            _rear.Next = nn;
            _rear = nn;
            _count++;

            _indicator = new KeyValuePair<int, Node>(-1, _head);
        }
        public T Dequeue()
        {
            if (_head.Next == _rear) { _rear = _head; }
            var v = _head.Next.Value;
            _head.Next = _head.Next.Next;
            _count--;

            _indicator = new KeyValuePair<int, Node>(-1, _head);

            return v;
        }
        public T Peek()
        {
            return _head.Next.Value;
        }

        public void Clear()
        {
            _rear = _head;
            _count = 0;

            _indicator = new KeyValuePair<int, Node>(-1, _head);
        }

        public bool Contains(T item)
        {
            var c = _head.Next;
            while (c != null)
            {
                if (item.Equals(c.Value)) { return true; }
                c = c.Next;
            }
            return false;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            var c = _head.Next;
            while (c != null)
            {
                array.SetValue(c.Value, index++);
                c = c.Next;
            }
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            var c = _head.Next;
            while (c != null)
            {
                array[arrayIndex++] = c.Value;
                c = c.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var c = _head.Next;
            while (c != null)
            {
                yield return c.Value;
                c = c.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        // {
        //     public T Current => throw new NotImplementedException();

        //     object IEnumerator.Current => Current;

        //     public void Dispose()
        //     {
        //         throw new NotImplementedException();
        //     }

        //     public bool MoveNext()
        //     {
        //         throw new NotImplementedException();
        //     }

        //     public void Reset()
        //     {
        //         throw new NotImplementedException();
        //     }
        // }

        private sealed class Node
        {
            public T Value;
            public Node Next;
            public IndexedQueue<T> Queue;
            public Node(T v, IndexedQueue<T> q)
            {
                Value = v;
                Queue = q;
            }
        }
    }
}
