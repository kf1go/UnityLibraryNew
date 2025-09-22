using System.Collections;
using UnityEngine;

public class IET3 : IEnumerator
{
    private int d;
    bool IEnumerator.MoveNext()
    {
        Debug.Log("ddm3");
        d++;
        return d < 3;
    }

    void IEnumerator.Reset()
    {
        throw new System.NotImplementedException();
    }

    object IEnumerator.Current
    {
        get
        {
            Debug.Log("wait frame");
            return null;
        }
    }
}
