using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

public static class CustomCoroutineRunner
{
    internal static readonly List<TupleCoroutineHandle> list = new List<TupleCoroutineHandle>(32);
    internal static readonly Dictionary<int, Func<bool>> moveNext = new Dictionary<int, Func<bool>>(32);

    internal static class TimerUpdate
    {
        public static void UpdateFunction()
        {
            for (int i = 0; i < list.Count; i++)
            {
                IEnumerator<Nullable<int>> item = list[i].generator;

                Func<bool> mn = list[i].mn;
                if (mn == null)
                {
                    bool moveNextResult = item.MoveNext();
                    if (!moveNextResult)
                    {
                        //list[i] =
                        continue;
                    }
                    Nullable<int> currentHash = item.Current;
                    if (currentHash.HasValue)
                    {
                        Func<bool> outValue;
                        mn = moveNext.TryGetValue(currentHash.Value, out outValue) ? outValue : null;
                    }
                }

                if (mn != null)
                {
                    if (!mn.Invoke())
                    {
                        mn = null;
                    }
                }

                TupleCoroutineHandle listOverride = list[i];
                listOverride.mn = mn;
                list[i] = listOverride;
            }
        }
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void InitSubsystem()
    {
        PlayerLoopSystem updateLoop = CustomPlayerLoop.CreateLoopSystem(typeof(TimeUpdate), TimerUpdate.UpdateFunction);
        CustomPlayerLoop.RegisterCustomLoop(typeof(Update), updateLoop);
    }
    public static int StartCoroutine(IEnumerator<Nullable<int>> generator)
    {
        int result = 11;
        TupleCoroutineHandle handle = new TupleCoroutineHandle();
        handle.generator = generator;
        list.Add(handle);
        return result;
    }
    public static void StopCoroutine(int coroutineHandle)
    {
        throw new NotImplementedException();
    }
}
