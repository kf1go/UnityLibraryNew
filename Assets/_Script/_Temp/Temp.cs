using System;
using System.Collections;
using System.Collections.Generic;
using Custom.Audio;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private void Start()
    {
        //CustomCoroutineRunner.StartCoroutine(Cor_Coroutine());
        //StartCoroutine(Cor_Coroutine());
        CustomCoroutineRunner.StartCoroutine(CustomCoroutineGenerator());
    }
    private IEnumerator<Nullable<int>> CustomCoroutineGenerator()
    {
        float endTime = 1 + Time.time;
        while (Time.time < endTime)
        {
            yield return null;
        }

        yield return new WaitDelay();
    }
    private IEnumerator<IEnumerator<TestCor>> Hi()
    {
        return null;
    }
    private IEnumerator<object> Cor_Coroutine()
    {
        Debug.Log("start");
        yield return new IEnumeratorTest();
        Debug.Log("end");
    }
}
