using System;
using UnityEngine;

public struct AntiClosureAction<Argument>
    where Argument : class
{
    private readonly Argument _arg;
    private Action<Argument> _callback;
    public bool HasValue { get; private set; }
    public bool IsUnityObjectDead => _arg == null;
    public AntiClosureAction(Argument arg, Action<Argument> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException($"{nameof(callback)} is null");
        }

        HasValue = true;

        _arg = arg;
        _callback = callback;
    }
    public void Fire()
    {
        Debug.Assert(_arg != null);
        _callback.Invoke(_arg);
    }
}
public struct AntiClosureAction<Argument, Arg1>
    where Argument : class
{
    private readonly Argument _arg;
    private Action<Argument, Arg1> _callback;
    public bool HasValue { get; private set; }
    public bool IsUnityObjectDead => _arg == null;
    public AntiClosureAction(Argument arg, Action<Argument, Arg1> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException($"{nameof(callback)} is null");
        }

        HasValue = callback != null;

        _arg = arg;
        _callback = callback;
    }
    public void Fire(Arg1 arg1)
    {
        Debug.Assert(_arg != null);
        _callback.Invoke(_arg, arg1);
    }
}
public struct AntiClosureAction<Argument, Arg1, Arg2>
    where Argument : class
{
    private readonly Argument _arg;
    private Action<Argument, Arg1, Arg2> _callback;
    public bool HasValue { get; private set; }
    public bool IsUnityObjectDead => _arg == null;
    public AntiClosureAction(Argument arg, Action<Argument, Arg1, Arg2> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException($"{nameof(callback)} is null");
        }

        HasValue = true;

        _arg = arg;
        _callback = callback;
    }
    public void Fire(Arg1 arg1, Arg2 arg2)
    {
        Debug.Assert(_arg != null);
        _callback.Invoke(_arg, arg1, arg2);
    }
}
public struct AntiClosureAction<Argument, Arg1, Arg2, Arg3>
    where Argument : class
{
    private readonly Argument _arg;
    private Action<Argument, Arg1, Arg2, Arg3> _callback;
    public bool HasValue { get; private set; }
    public bool IsUnityObjectDead => _arg == null;
    public AntiClosureAction(Argument arg, Action<Argument, Arg1, Arg2, Arg3> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException($"{nameof(callback)} is null");
        }

        HasValue = true;

        _arg = arg;
        _callback = callback;
    }
    public void Fire(Arg1 arg1, Arg2 arg2, Arg3 arg3)
    {
        Debug.Assert(_arg != null);
        _callback.Invoke(_arg, arg1, arg2, arg3);
    }
}