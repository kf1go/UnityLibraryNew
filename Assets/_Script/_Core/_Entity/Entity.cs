using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Dictionary<Type, IEntityComponent> componentDictionary;
    protected virtual int ComponentCount { get; } = -1;
    protected bool isComponentInitialized;
    private const int k_defaultComponentCount = 16;
    //initializing at start because of Default Awake calls
    protected virtual void Start()
    {
        int initCount = ComponentCount;
        if (ComponentCount < 1)
        {
            initCount = k_defaultComponentCount;
#if UNITY_EDITOR
            Debug.LogWarning("component count not specified");
#endif
        }
        componentDictionary = new Dictionary<Type, IEntityComponent>(initCount);

        isComponentInitialized = false;
        IEntityComponent[] componentCollection = GetComponentsInChildren<IEntityComponent>(true);
        InitializeComponents(componentCollection);
    }
    protected abstract void InitializeComponents(IEntityComponent[] componentColection);
    public EntityComponentType GetEntityComponent<EntityComponentType>()
        where EntityComponentType : MonoBehaviour, IEntityComponent
    {
        Type targetType = typeof(EntityComponentType);
        Debug.Assert(isComponentInitialized, $"trying to reference {targetType.Name} when it's not initilaized");

        if (componentDictionary.TryGetValue(targetType, out IEntityComponent value))
        {
            return (EntityComponentType)value;
        }

        Debug.LogError($"can't find {targetType.Name}", this);
        return null;
    }
}

/// <summary>
/// Entity class that can reference <see cref="IEntityComponent"/>, <see cref="IEntityComponentAwake{T}"/>, <see cref="IEntityComponentStart{T}"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Entity<T> : Entity
    where T : Entity<T>
{
    protected override void InitializeComponents(IEntityComponent[] componentColection)
    {
        IEntityComponent[] componentCollection = GetComponentsInChildren<IEntityComponent>(true);

        foreach (IEntityComponent item in componentCollection)
        {
            componentDictionary.Add(item.GetType(), item);
        }
        T entity = (T)this;
        foreach (IEntityComponentAwake<T> item in componentCollection.OfType<IEntityComponentAwake<T>>())
        {
            item.EntityComponentAwake(entity);
        }
        isComponentInitialized = true;
        foreach (IEntityComponentStart<T> item in componentCollection.OfType<IEntityComponentStart<T>>())
        {
            item.EntityComponentStart(entity);
        }
    }
}