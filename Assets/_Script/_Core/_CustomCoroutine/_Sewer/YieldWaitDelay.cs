using UnityEngine;

public class YieldWaitDelay : CustomYieldInstruction_
{
    public override object Current => null;
    private float endTime;
    public YieldWaitDelay(float delay)
    {
        endTime = Time.time + delay;
    }
    public override bool MoveNext()
    {
        return Time.time < endTime;
    }
}
