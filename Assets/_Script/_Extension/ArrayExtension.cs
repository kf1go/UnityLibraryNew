using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtension
{
    public static T GetClosestItem<T>(this IReadOnlyList<T> collection, Vector3 target)
        where T : Component
    {
        T result = null;
        int collectionCount = collection.Count;
        float minDistanceSqr = float.MaxValue;
        for (int i = 0; i < collectionCount; i++)
        {
            T item = collection[i];
            Vector3 itemToTarget = target - item.transform.position;
            float itemDistanceSqr = itemToTarget.sqrMagnitude;
            if (minDistanceSqr > itemDistanceSqr) // use >= to get last item
            {
                result = item;
                minDistanceSqr = itemDistanceSqr;
            }
        }
        return result;
    }
    public static T GetClosestItem<T>(this IReadOnlyList<T> collection, Vector3 target, int collectionCount)
        where T : Component
    {
        T result = null;
        float minDistanceSqr = float.MaxValue;
        for (int i = 0; i < collectionCount; i++)
        {
            T item = collection[i];
            Vector3 itemToTarget = target - item.transform.position;
            float itemDistanceSqr = itemToTarget.sqrMagnitude;
            if (minDistanceSqr > itemDistanceSqr) // use >= to get last item
            {
                result = item;
                minDistanceSqr = itemDistanceSqr;
            }
        }
        return result;
    }

}
