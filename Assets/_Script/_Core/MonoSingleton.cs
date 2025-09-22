using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public abstract class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>
{
    protected virtual MonoSingletonFlags SingletonFlag { get; } // consider using attribute to get flags in static scope
    private static bool isPlayerClosing;

    public static T Instance
    {
        get
        {
            if (System.Object.ReferenceEquals(_instance, null)) //using C# null check
            {
                if (isPlayerClosing)
                {
                    return null;
                }

                _instance = RuntimeInitialize();
            }
            return _instance;
        }
    }
    private static T _instance = null;

#if UNITY_EDITOR
    private bool flag_AwakeDone;
#endif

    protected virtual void Awake()
    {
#if UNITY_EDITOR
        if (flag_AwakeDone)
        {
            Debug.LogError("calling awake in different scope", gameObject);
            return;
        }
#endif

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
#if UNITY_EDITOR
        if (HasFlag(SingletonFlag, MonoSingletonFlags.Hide))
        {
            gameObject.hideFlags = HideFlags.HideInHierarchy;
        }

        Debug.Log($"[Singleton_Awake] [type : {typeof(T).Name}] [name : {gameObject.name}]");
        flag_AwakeDone = true;
#endif

        _instance = (T)this;
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
        isPlayerClosing = true;
    }
    private static T RuntimeInitialize()
    {
        GameObject gameObject = new GameObject();
        T result = gameObject.AddComponent<T>();

#if UNITY_EDITOR
        string singletonMessage = "Runtime_Singleton" + typeof(T).Name;
        gameObject.name = singletonMessage;

        if (HasFlag(result.SingletonFlag, MonoSingletonFlags.DBG_DontAutoCreate))
        {
            _instance = null;
            throw new InvalidOperationException("singleton is tagged as dont auto create");
        }
        else
        {
            Debug.LogWarning(singletonMessage, gameObject);
        }
#endif

        return result;
    }
    private static bool HasFlag(MonoSingletonFlags flag, MonoSingletonFlags target)
    {
        return (flag & target) > 0;
    }
}