using System;
using System.Collections.Generic;
using UnityEngine;

namespace Custom.Pool
{
    public abstract class ObjectPoolBase<T>
        where T : class
    {
#if UNITY_EDITOR
        private readonly HashSet<T> collisionHashSet;
#endif

        private readonly int maxCapacity;
        protected readonly Stack<T> poolStack;
#if UNITY_EDITOR
        protected AntiClosureAction<ObjectPoolBase<T>, int> OnPoolMaxCapacityReached;
#endif

        public ObjectPoolBase(int initialPoolCapacity = 16, int maxCapacity = 1000)
        {
            this.maxCapacity = maxCapacity;
            poolStack = new Stack<T>(initialPoolCapacity);
#if UNITY_EDITOR
            collisionHashSet = new HashSet<T>(initialPoolCapacity);
#endif
        }
        public virtual T Pop()
        {
            T result;
            if (poolStack.Count == 0)
            {
                result = Create();
            }
            else
            {
                result = poolStack.Pop();
#if UNITY_EDITOR
                collisionHashSet.Remove(result);
#endif
            }
            return result;
        }
        public virtual void Push(T instance)
        {
            Debug.Assert(instance != null);

#if UNITY_EDITOR
            bool collision = collisionHashSet.Contains(instance);
            if (collision)
            {
                throw new ArgumentException($"Collision Detected. prefab : {instance}, {typeof(T)}_pool");
            }

            collisionHashSet.Add(instance);
#endif
            if (poolStack.Count < maxCapacity)
            {
                poolStack.Push(instance);
            }
            else
            {
#if UNITY_EDITOR
                if (OnPoolMaxCapacityReached.HasValue) 
                {
                    OnPoolMaxCapacityReached.Fire(maxCapacity);
                }
#endif
                Destroy(instance);
            }
        }
        public virtual void Clear()
        {
            poolStack.Clear();
#if UNITY_EDITOR
            collisionHashSet.Clear();
#endif
        }
        protected abstract T Create();
        protected abstract void Destroy(T instance);
    }
}
