using System;
using System.Collections;
using UnityEngine;

public abstract class CustomYieldInstruction_ : IEnumerator
{
    public abstract object Current { get; }
    public abstract bool MoveNext();
    void IEnumerator.Reset()
    {
        throw new System.NotImplementedException();
    }
}
