using System.Collections.Generic;
using UnityEngine;

namespace Custom.Pool
{
    public static class GameObjectPoolManager
    {
        private readonly static Dictionary<int, GameObjectPool> gameObjectPoolDictionary;
        private const int k_DictionaryCapacitiy = 10; // TODO
        static GameObjectPoolManager()
        {
            gameObjectPoolDictionary = new Dictionary<int, GameObjectPool>(k_DictionaryCapacitiy);
        }
        public static void InitializePool(PoolGameObjectSO prefabSO)
        {
            int hash = prefabSO.Hash;
            GameObject prefab = prefabSO.Prefab;

            GameObjectPool result = new GameObjectPool(prefabSO.Prefab, prefabSO.InitialPoolCapacity, prefabSO.MaxCapacity);
            gameObjectPoolDictionary.Add(prefabSO.Hash, result);
        }
        public static GameObject Pop(PoolGameObjectSO prefabSO)
        {
            Debug.Assert(prefabSO != null);

            int hash = prefabSO.Hash;
            GameObjectPool pool = gameObjectPoolDictionary[hash];
            Debug.Assert(pool != null);

            GameObject result = pool.Pop();
            //if (gameObjectPoolDictionary.TryGetValue(prefabSO.Hash, out GameObjectPool value))
            //{
            //    result = value.Pop();
            //}
            //else
            //{
            //    Debug.LogWarning("runtimeInitializing! call func:Initialize before calling this");
            //    GameObjectPool gameObjectPool = CreateDictionary(prefabSO);
            //    result = gameObjectPool.Pop();
            //}
            return result;
        }
        public static void Push(PoolGameObjectSO prefabSO, GameObject instance)
        {
            Debug.Assert(prefabSO != null);
            Debug.Assert(instance != null, "local: push instance is null");

            int hash = prefabSO.Hash;
            GameObjectPool pool = gameObjectPoolDictionary[hash];
            Debug.Assert(pool != null);

            pool.Push(instance);

            //if (gameObjectPoolDictionary.TryGetValue(prefabSO.Hash, out GameObjectPool value))
            //{
            //    value.Push(instance);
            //}
            //else
            //{
            //    Debug.LogWarning("runtimeInitializing! call func:Initialize before calling this");
            //    GameObjectPool gameObjectPool = CreateDictionary(prefabSO);
            //    gameObjectPool.Push(instance);
            //}
        }
        public static void Clear(PoolGameObjectSO prefabSO)
        {
            Debug.Assert(prefabSO != null);
            gameObjectPoolDictionary[prefabSO.Hash].Clear();
        }
        public static void ClearAll()
        {
            foreach (GameObjectPool item in gameObjectPoolDictionary.Values)
            {
                item.Clear();
            }
        }
    }
}
