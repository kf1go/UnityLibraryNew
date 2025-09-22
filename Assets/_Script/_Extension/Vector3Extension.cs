using System.Runtime.CompilerServices;
using UnityEngine;

public static class Vector3Extension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInRange(this Vector3 vector, float sqrRange)
    {
        bool result = vector.sqrMagnitude <= sqrRange;
        return result;
    }
}
