using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CustomCoroutineHandle2
{
    public Stack<IEnumerator> CoroutineStack { get; private set; }
    public bool isRunning { get; set; }
    public CustomCoroutineHandle2(int capacity)
    {
        CoroutineStack = new Stack<IEnumerator>(capacity);
        isRunning = false;
    }
    public bool MoveNext()
    {
        if (CoroutineStack.Count < 1)
        {
            // delete coroutine
            return false;
        }

        IEnumerator item = CoroutineStack.Peek();
        HandleCoroutine(item);

        return true;
    }
    private void HandleCoroutine(IEnumerator coroutine)
    {
        if (coroutine.MoveNext())
        {
            object current = coroutine.Current;
            if (current == null)
            {
                return;
            }

            if (current is IEnumerator nestedCoroutine)
            {
                CoroutineStack.Push(nestedCoroutine);
                HandleCoroutine(nestedCoroutine);
            }
        }
        else
        {
            CoroutineStack.Pop();
            if (CoroutineStack.Count > 0)
            {
                HandleCoroutine(CoroutineStack.Peek());
            }
        }
    }
}
