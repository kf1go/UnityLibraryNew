using System.Collections;
using UnityEngine;

public class IEnumeratorTest : IEnumerator
{
    private int ind = -1;
    bool IEnumerator.MoveNext()
    {
        ind++;
        Debug.Log("moveNe");
        return ind < 3;
    }
    void IEnumerator.Reset()
    {
        throw new System.NotImplementedException();
    }
    object IEnumerator.Current
    {
        get
        {
            Debug.Log("cur");
            object result = null;
            switch (ind)
            {
                case 0:
                    result = new WaitForSeconds(1);
                    break;
                case 1:
                    result = new IET2();
                    break;
                case 2:
                    result = new WaitForSeconds(1);
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
