using System;
using UnityEngine;

namespace Custom.Pool
{
    public static class MonoGenericPool<T>
        where T : MonoBehaviour, IPoolable
    {
        private static MonoPool<T> monoPool;
        /// <summary>
        /// This function must be called before any other methods are invoked.
        /// </summary>
        public static void Initialize(PoolMonoBehaviourSO prefabSO)
        {
            Debug.Assert(prefabSO != null);

            if (monoPool != null)
            {
#if UNITY_EDITOR
                Debug.LogError($"monoPool is already initialized. {prefabSO.name}");
#endif
                return;
            }
            
            T prefab = (T)prefabSO.GetMono;
            monoPool = new MonoPool<T>(prefab, prefabSO.InitialPoolCapacity, prefabSO.MaxCapacity);

#if UNITY_EDITOR
            Debug.Assert(prefab != null, $"failed to cast Mono to T. {prefabSO.name}", prefabSO);
#endif
        }
        public static T Pop()
        {
            return monoPool.Pop();
        }
        public static void Push(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("push instance is null");
            }
            monoPool.Push(instance);
        }
        public static void Clear()
        {
            monoPool.Clear();
        }
    }
}
