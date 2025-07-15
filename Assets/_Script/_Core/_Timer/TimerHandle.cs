using System;
using UnityEngine;

public class TimerHandle<T> : TimerHandleBase
    where T : UnityEngine.Object
{
    /// <summary>
    /// note that this won't be called when timer is killed
    /// </summary>
    public Action<TimerHandle<T>> OnCompleteCallback;
    /// <summary>
    /// update callback
    /// </summary>
    public Action<TimerHandle<T>> OnUpdate;
    /// <summary>
    /// fires when killed
    /// </summary>
    public Action<TimerHandle<T>> OnKill;

    public readonly T target;
    internal TimerHandle(T target, float duration)
        : base(target, duration)
    {
        if (target == null)
        {
            throw new ArgumentNullException($"{target} is null");
        }

        this.target = target;
    }
    internal override void Update()
    {
        base.Update();
        if (OnUpdate != null)
        {
            OnUpdate.Invoke(this);
        }
    }
    protected override void End()
    {
        base.End();
        if (OnCompleteCallback != null)
        {
            OnCompleteCallback(this);
        }
    }
    public override void Kill()
    {
        base.Kill();
        if (OnKill != null)
        {
            OnKill.Invoke(this);
        }
        OnCompleteCallback = null;
        OnUpdate = null;
        OnKill = null;
    }
}
