using System;
using System.Collections.Generic;

namespace GF47RunTime
{
    public class Pool<T> where T : class 
    {
        private Queue<T> _queue;
        private Func<T> _createNewFunc;
        private Action<T> _resetAction;

        public void Initialize(int count, Func<T> createNewFunc, Action<T> resetAction = null)
        {
            _queue = new Queue<T>(count);
            _createNewFunc = createNewFunc;
            _resetAction = resetAction;
            for (int i = 0; i < count; i++)
            {
                T item = _createNewFunc();
                if (_resetAction != null) { _resetAction(item); }
                _queue.Enqueue(item);
            }
        }

        public T Get()
        {
            if (_queue.Count == 0)
            {
                T item = _createNewFunc();
                if (_resetAction != null)
                {
                    _resetAction(item);
                }
                return item;
            }
            return _queue.Dequeue();
        }

        public T[] Get(int count)
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = Get();
            }
            return result;
        }

        public void Reset(T target)
        {
            if (target == null)
            {
#if DEBUG
                throw new ArgumentNullException("target", "将空值放入了池中");
#else
                return;
#endif
            }
            if (_resetAction != null) { _resetAction(target); }
            _queue.Enqueue(target);
        }

        public void Reset(ICollection<T> targets)
        {
            foreach (T target in targets)
            {
                Reset(target);
            }
        }
    }
}