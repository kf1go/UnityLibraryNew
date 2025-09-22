using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

public static class CustomCoroutineRunner2
{
    internal static readonly List<CustomCoroutineHandle2> list = new List<CustomCoroutineHandle2>(32);
    private const int k_nestedCoroutineStackCount = 4;

    static CustomCoroutineRunner2()
    {
        for (int i = 0; i < list.Capacity; i++)
        {
            CustomCoroutineHandle2 handle = new CustomCoroutineHandle2(k_nestedCoroutineStackCount);
            list.Add(handle);
        }
    }

    internal static class TimerUpdate
    {
        public static void UpdateFunction()
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                CustomCoroutineHandle2 item = list[i];

                if (!item.isRunning)
                {
                    // TODO : 
                    //remove element at once?
                    continue;
                }

                if (!item.MoveNext())
                {
                    CustomCoroutineHandle2 itemOverride = list[i];
                    itemOverride.isRunning = false;
                    list[i] = itemOverride;
                }
            }
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void InitSubsystem()
    {
        PlayerLoopSystem updateLoop = CustomPlayerLoop.CreateLoopSystem(typeof(Update), TimerUpdate.UpdateFunction);
        CustomPlayerLoop.RegisterCustomLoop(typeof(Update), updateLoop);
    }
    public static void StartCoroutine(IEnumerator generator)
    {
        CustomCoroutineHandle2 handle = new CustomCoroutineHandle2();
        bool flag_found = false;
        for (int i = 0; i < list.Capacity; i++)
        {
            CustomCoroutineHandle2 item = list[i];
            if (!item.isRunning)
            {
                handle = item;
                flag_found = true;
            }
        }

        if (!flag_found)
        {
            handle = new CustomCoroutineHandle2(k_nestedCoroutineStackCount);
        }

        handle.CoroutineStack.Push(generator);
        list.Add(handle);
    }
    public static void StopCoroutine(int coroutineHandle)
    {
        throw new NotImplementedException();
    }
}
