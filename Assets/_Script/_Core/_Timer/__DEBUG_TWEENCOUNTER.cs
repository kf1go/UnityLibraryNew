using UnityEngine;

[DefaultExecutionOrder(-300)]
internal class __DEBUG_TWEENCOUNTER : MonoBehaviour
{
    private void Update()
    {
        int cnt = TimerRunner.TimerUpdate.GetTimers.Count;
        IMGUIMono.DebugText(new DebugInfo($"timer cnt : {cnt}"), key: DebugKey.DefaultFlag);
    }
}
