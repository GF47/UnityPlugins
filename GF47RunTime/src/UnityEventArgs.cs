namespace GF47RunTime 
{
    using System;

    public class UnityEventArgs<T> : EventArgs
    {
        public T value;
        public UnityEventArgs(T value) { this.value = value; }
        public UnityEventArgs() : this(default(T)) { }

        public override string ToString()
        {
            return string.Format("type={0},value={1}", typeof(T), value);
        }
    }
}
