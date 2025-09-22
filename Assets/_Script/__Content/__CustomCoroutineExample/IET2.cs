using System.Collections;
using UnityEngine;

public class IET2 : IEnumerator
{
    private int id2 = 0;
    bool IEnumerator.MoveNext()
    {
        Debug.Log("ddmove");
        id2++;
        return id2 < 3;
    }
    object IEnumerator.Current
    {
        get
        {
            Debug.Log("cur2");
            object result = null;
            switch (id2)
            {
                case 0:
                    result = new WaitForSeconds(1);
                    break;
                case 1:
                    result = new IET3();
                    break;
                case 2:
                    result = new YieldWaitDelay(2);
                    break;
                default:
                    break;
            }
            return result;
        }
    }

    void IEnumerator.Reset()
    {
        throw new System.NotImplementedException();
    }
}
