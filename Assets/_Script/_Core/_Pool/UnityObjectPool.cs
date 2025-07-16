using UnityEngine;
using UnityEngine.SceneManagement;

namespace Custom.Pool
{
    internal static class UnityObjectPool
    {
        private static Transform baseParent;
        internal static event System.Action SceneChangePoolDestroyEvent;
        static UnityObjectPool()
        {
            CreateBaseParent();

            SceneManager.sceneLoaded += static (_, _) =>
            {
                if (baseParent == null)
                {
                    if (SceneChangePoolDestroyEvent != null)
                    {
                        SceneChangePoolDestroyEvent.Invoke();
                    }
                    CreateBaseParent();
                }
            };
            static void CreateBaseParent()
            {
                baseParent = new GameObject("_UnityObjectPool_BaseParent").transform;
            }
        }
    }
    internal abstract class UnityObjectPool<T> : ObjectPoolBase<T>
        where T : Object
    {
        protected readonly T unityObject;
        public UnityObjectPool(T prefab, int initialPoolCapacity = 16, int maxCapacity = 1000)
            : base(initialPoolCapacity, maxCapacity)
        {
            Debug.Assert(prefab != null);

#if UNITY_EDITOR
            OnPoolMaxCapacityReached = new AntiClosureAction<ObjectPoolBase<T>, int>(this,
                static (ObjectPoolBase<T> @this, int maxCapacitiy) =>
                {
                    UnityObjectPool<T> unityObjectPool = (UnityObjectPool<T>)@this;
                    Debug.LogWarning($"{unityObjectPool.unityObject.name} reached max pool capacity", unityObjectPool.unityObject);
                });
#endif
            this.unityObject = prefab;
        }
        protected override T Create()
        {
            T result = Object.Instantiate(unityObject);
            result.hideFlags = HideFlags.HideInHierarchy;
            return result;
        }
        protected override void Destroy(T instance)
        {
            Debug.Assert(instance != null);
            Object.Destroy(instance);
        }
    }
}
