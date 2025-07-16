using System;
using UnityEngine;

public struct DebugInfo
{
    public bool Is3D => worldPosition.HasValue;
    public Nullable<Vector3> worldPosition;
    public string message;

    public DebugInfo(string message, Nullable<Vector3> worldPosition = null)
    {
        this.worldPosition = worldPosition;
        this.message = message;
    }
    public readonly Vector3 GetScreenPos(Camera camera)
    {
        Debug.Assert(worldPosition.HasValue, "world position is null");
        Vector3 result = camera.WorldToScreenPoint(worldPosition.Value, Camera.MonoOrStereoscopicEye.Mono);
        return result;
    }
}
