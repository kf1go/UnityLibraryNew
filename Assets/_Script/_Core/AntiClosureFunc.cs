using System;
using UnityEngine;

/// <summary>
/// seems like this is useless
/// </summary>
/// <typeparam name="Argument"></typeparam>
/// <typeparam name="Result"></typeparam>
public struct AntiClosureFunc<Argument, Result>
    where Argument : class
{
    private readonly Argument _arg;
    private Func<Argument, Result> _selector;
    public bool HasValue { get; private set; }
    public AntiClosureFunc(Argument arg, Func<Argument, Result> selector)
    {
        if (selector == null)
        {
            throw new ArgumentNullException($"{nameof(selector)} is null");
        }

        HasValue = true;

        _arg = arg;
        _selector = selector;
    }
    public Result Get()
    {
        Result result = _selector.Invoke(_arg);
        return result;
    }
}
