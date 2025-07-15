using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected virtual MonoSingletonFlags SingletonFlag { get; } // consider using attribute to get flags from static scope
    private static T _instance = null;

    private static bool IsShuttingDown { get; set; }
    public static T Instance
    {
        get
        {
            if (System.Object.ReferenceEquals(_instance, null)) //using C# null check
            {
                if (IsShuttingDown)
                {
                    return null;
                }

                _instance = RuntimeInitialize();
            }
            return _instance;
        }
    }
    private static T RuntimeInitialize()
    {
        string singletonMessage = string.Empty; 
#if UNITY_EDITOR
        singletonMessage = "Runtime_Singleton" + typeof(T).Name;
#endif
        GameObject gameObject = new GameObject(name: singletonMessage);
        T result = gameObject.AddComponent<T>();

#if UNITY_EDITOR
        if (HasFlag(result.SingletonFlag, MonoSingletonFlags.DBG_DontAutoCreate))
        {
            throw new InvalidOperationException("singleton is tagged as dont auto create");
        }
#endif

        Debug.LogWarning(singletonMessage, gameObject);
        return result;
    }
    protected virtual void Awake()
    {
        //check two singleton error
        if (!System.Object.ReferenceEquals(_instance, null)) //using System.Object null check
        {
            Debug.LogError("TwoSingletons_" + typeof(T).Name, this);
            Destroy(gameObject);
            return;
        }

        //custom singleton attribute setting
        if (HasFlag(SingletonFlag, MonoSingletonFlags.DontDestroyOnLoad))
        {
            DontDestroyOnLoad(gameObject);
        }
        if (HasFlag(SingletonFlag, MonoSingletonFlags.Hide))
        {
            gameObject.hideFlags = HideFlags.HideInHierarchy;
        }

#if UNITY_EDITOR
        Debug.Log($"[Singleton_Awake] [type : {typeof(T).Name}] [name : {gameObject.name}]");
#endif
        _instance = (T)this;
    }
    private static bool HasFlag(MonoSingletonFlags flag, MonoSingletonFlags target)
    {
        return (flag & target) > 0;
    }
    protected virtual void OnDestroy()
    {
        if (System.Object.ReferenceEquals(_instance, this))
        {
            _instance = null; //explicitly setting null for C# null check
        }
    }
    protected virtual void OnApplicationQuit()
    {
        IsShuttingDown = true;
    }
}