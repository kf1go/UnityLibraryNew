using System;
using UnityEngine;

public struct WaitDelay : ICustomYield
{
    private float endTime;
    internal int Hash { get; set; }

    public WaitDelay(float delay)
    {
        endTime = Time.time + delay;
        Hash = 123;

        CustomCoroutineRunner.moveNext.Add(Hash, MoveNext);
    }
    public bool MoveNext()
    {
        bool result = Time.time < endTime;
        return result;
    }
    public static implicit operator int(WaitDelay yield) => yield.Hash;
}
