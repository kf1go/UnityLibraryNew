using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MonoTest : MonoBehaviour
{
    private void Awake()
    {
        //StartCoroutine(RegularCoroutine());
        CustomCoroutineRunner2.StartCoroutine(RegularCoroutine());
    }
    private IEnumerator RegularCoroutine()
    {
        //Debug.Log("start");
        yield return null;
        yield return new IET2();
        Debug.Log("end");
    }
    private IEnumerator<Nullable<int>> CorTest()
    {
        Debug.Log("start");
        yield return null;
        yield return new WaitDelay(1);
        Debug.Log("end");
    }
}
