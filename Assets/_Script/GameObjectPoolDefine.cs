using System;
using System.Collections.Generic;
using Custom.Pool;
using UnityEngine;

public enum EPoolGameObjectType
{
    //Particle
    Particle,
    ParticleBullet,
}
public enum EParticleType
{
    Explosion
}
public enum EParticleBulletType
{
    Metal,
    Blood,
    Blood_HS
}

public class GameObjectPoolDefine : MonoSingleton<GameObjectPoolDefine>
{
    protected override MonoSingletonFlags SingletonFlag => MonoSingletonFlags.DontDestroyOnLoad | MonoSingletonFlags.DBG_DontAutoCreate;
    public static IReadOnlyDictionary<int, PoolGameObjectSO>[] GetPool => poolGameObjectSO;
    private static readonly Dictionary<int, PoolGameObjectSO>[] poolGameObjectSO = new Dictionary<int, PoolGameObjectSO>[GetEnumLength<EPoolGameObjectType>()];

    [SerializeField] private PoolGameObjectSO[] particleTypeCollection;
    [SerializeField] private PoolGameObjectSO[] particleBulletCollection;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }
    public void Initialize()
    {
        poolGameObjectSO[(int)EPoolGameObjectType.Particle] = MakeDictionary<EParticleType>(particleTypeCollection);
        poolGameObjectSO[(int)EPoolGameObjectType.ParticleBullet] = MakeDictionary<EParticleBulletType>(particleBulletCollection);

        return;

        static Dictionary<int, PoolGameObjectSO> MakeDictionary<EnumType>(PoolGameObjectSO[] gameObjectCollection)
            where EnumType : Enum
        {
            int maxEnumLength = GetEnumLength<EnumType>();
            
            Dictionary<int, PoolGameObjectSO> result = new Dictionary<int, PoolGameObjectSO>(maxEnumLength);
            for (int i = 0; i < maxEnumLength; i++)
            {
                PoolGameObjectSO item = gameObjectCollection[i];
                GameObjectPoolManager.InitializePool(item);
                result[i] = item;
            }

            return result;
        }
    }
    public static IReadOnlyDictionary<int, PoolGameObjectSO> GetPoolGameObjectDictionary(EPoolGameObjectType poolGameObjectType)
    {
        Dictionary<int, PoolGameObjectSO> result = poolGameObjectSO[(int)poolGameObjectType];
        return result;
    }
    private static int GetEnumLength<EnumType>()
        where EnumType : Enum
    {
        int result = Enum.GetValues(typeof(EnumType)).Length;
        return result;
    }
}
