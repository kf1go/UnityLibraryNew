using System.Collections;
using UnityEngine;

public class TestCor : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Test2());
    }
    private IEnumerator Test2()
    {
        yield return new WaitForSeconds(1000);
    }
}
