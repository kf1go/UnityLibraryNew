using System;
using UnityEngine;

/// <summary>
/// dont use this, this class is only used for internal usage but public because of inheritance
/// </summary>
public abstract class TimerHandleBase
{
    public float EndTime
    {
        get => backingFieldEndtime;
        internal set
        {
            backingFieldEndtime = value;
            IsCompleted = false;
        }
    }
    private float backingFieldEndtime;

    public bool IsCompleted { get; private set; }

    /// <summary>
    /// incomplete.
    /// todo : if endtime is changed this will be updated.
    /// </summary>
    public float InitializeTime => EndTime - Duration;
    public float TimeLeft => EndTime - Time.time;
    public float Duration { get; private set; }

    private readonly UnityEngine.Object target;
    internal TimerHandleBase(UnityEngine.Object unityObject, float duration)
    {
        if (unityObject == null) throw new ArgumentNullException($"{unityObject} is null");

        Duration = duration;
        target = unityObject;
        EndTime = duration + Time.time;
    }
    internal virtual void Update()
    {
        if (IsCompleted)
        {
            return;
        }

        bool targetDestroyed = target == null; //todo : this is expensive
        bool isTimeOut = Time.time > EndTime;

        if (isTimeOut)
        {
            End();
        }

        if (targetDestroyed || isTimeOut)
        {
            Kill();
        }
    }
    protected virtual void End()
    {
    }
    public virtual void Kill()
    {
        IsCompleted = true;
    }
}
