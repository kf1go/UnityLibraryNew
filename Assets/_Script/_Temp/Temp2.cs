using System.Collections;
using Custom.Pool;
using UnityEngine;

public class Temp2 : MonoBehaviour, IPoolable
{
    private void Awake()
    {
        Debug.Log("awa");
    }
    private void Start()
    {
        Debug.Log("start");
        StartCoroutine(Test());
        IEnumerator Test()
        {
            Debug.Log("startCor");
            StopAllCoroutines();
            yield return null;
            Debug.Log("stopcor");
        }
    }
}
