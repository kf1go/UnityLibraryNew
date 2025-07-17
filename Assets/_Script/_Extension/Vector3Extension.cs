using System.Runtime.CompilerServices;
using UnityEngine;

public static class Vector3Extension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInRange(this Vector3 vector, float range)
    {
        bool result = vector.sqrMagnitude < range * range;
        return result;
    }
}
