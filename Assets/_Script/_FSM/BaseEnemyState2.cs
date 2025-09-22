using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemyState2<T> : State<BaseEnemyState2<T>>
{
    protected T Owner { get; private set; }
    protected BaseEnemyState2(FiniteStateMachine<BaseEnemyState2<T>> finiteStateMachine, T owner) : base(finiteStateMachine)
    {
        Owner = owner;
    }
    public static bool UpdateNavMeshPath(NavMeshAgent navMeshAgent, NavMeshPath path, Vector3 targetPosition)
    {
        bool foundPath = navMeshAgent.CalculatePath(targetPosition, path);
        Debug.Assert(foundPath, "cant find path");
        //Vector3 result = foundPath ? path.corners[1] : targetPosition; // TODO : is foundPath typically true?

#if UNITY_EDITOR
        for (int i = 0; i < path.corners.Length; i++)
        {
            Vector3 position = path.corners[i];
            Debug.DrawRay(position, Vector3.up * 5, Color.red, 1);
        }
#endif
        return foundPath;
    }
    public static bool IsPlayerDetected(Transform startTransform, Vector3 targetPosition, float allowedAngle)
    {
        Vector3 forwardVector = startTransform.forward;
        Vector3 enemyToPlayer = targetPosition - startTransform.position;
        forwardVector.y = 0;
        enemyToPlayer.y = 0;

        enemyToPlayer.Normalize();

        float dotResult = Vector3.Dot(forwardVector, enemyToPlayer);
        float angle = Mathf.Acos(dotResult) * Mathf.Rad2Deg;
        bool result = angle > allowedAngle;
        return result;
    }
}
