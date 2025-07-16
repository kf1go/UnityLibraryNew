using System;
using System.Collections.Generic;
using Custom.Pool;
using UnityEngine;

public enum PoolGameObjectType
{
    Particle,
    Idk,
}
public enum ParticleType
{
    Metal,
    Explosion,
    Wood
}

public class GameObjectPoolDefine : MonoSingleton<GameObjectPoolDefine>
{
    protected override MonoSingletonFlags SingletonFlag => MonoSingletonFlags.DontDestroyOnLoad | MonoSingletonFlags.DBG_DontAutoCreate;
    public IReadOnlyDictionary<int, PoolGameObjectSO>[] GetPool => _poolGameObjectSO;
    private Dictionary<int, PoolGameObjectSO>[] _poolGameObjectSO = new Dictionary<int, PoolGameObjectSO>[GetEnumLength<ParticleType>()];

    [SerializeField] private PoolGameObjectSO[] _particleTypeCollection;

    private static int GetEnumLength<EnumType>()
        where EnumType : Enum
    {
        int result = GetEnumLength(typeof(EnumType));
        return result;
    }
    private static int GetEnumLength(Type enumType)
    {
        int result = Enum.GetValues(enumType).Length;
        return result;
    }
    public void Initialize()
    {
        _poolGameObjectSO[(int)PoolGameObjectType.Particle] = MakeDictionary<ParticleType>(_particleTypeCollection);
    }
    public IReadOnlyDictionary<int, PoolGameObjectSO> GetPoolGameObjectDictionary(PoolGameObjectType poolGameObjectType)
    {
        return _poolGameObjectSO[(int)poolGameObjectType];
    }
    private static Dictionary<int, PoolGameObjectSO> MakeDictionary<EnumType>(PoolGameObjectSO[] gameObjectCollection)
        where EnumType : Enum
    {
        int maxEnumLength = GetEnumLength<EnumType>();
        Dictionary<int, PoolGameObjectSO> result = new Dictionary<int, PoolGameObjectSO>(maxEnumLength);
        for (int i = 0; i < maxEnumLength; i++)
        {
            result[i] = InitPool(gameObjectCollection[i]);
        }
        return result;
    }
    private static PoolGameObjectSO InitPool(PoolGameObjectSO poolGameObjectSO)
    {
        GameObjectPoolManager.InitializePool(poolGameObjectSO);
        return poolGameObjectSO;
    }
}
