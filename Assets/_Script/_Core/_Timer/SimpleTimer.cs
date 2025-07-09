using System;
using UnityEngine;

[Serializable]
public struct SimpleTimer
{
    public float nextTime;
    public void Set(float delay)
    {
        nextTime = Time.time + delay;
    }
    public void Add(float delay)
    {
        nextTime += delay;
    }
    public bool IsTimerOver()
    {
        bool result = Time.time > nextTime;
        return result;
    }
}
