using System;
using System.Collections.Generic;
using UnityEngine;

public struct TupleCoroutineHandle
{
    public IEnumerator<Nullable<int>> generator;
    public Func<bool> mn;
}
