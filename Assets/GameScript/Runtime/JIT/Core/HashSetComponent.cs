using System;
using System.Collections.Generic;

namespace Game.JIT
{
    public class HashSetComponent<T>: HashSet<T>, IDisposable
    {
        public static HashSetComponent<T> Create()
        {
            return MonoPool.Instance.Fetch(typeof (HashSetComponent<T>)) as HashSetComponent<T>;
        }

        public void Dispose()
        {
            this.Clear();
            MonoPool.Instance.Recycle(this);
        }
    }
}