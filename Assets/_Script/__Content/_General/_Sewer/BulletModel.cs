
using Custom.Pool;
using UnityEngine;

public class BulletModel : MonoBehaviour, IPoolable
{
    private Rigidbody rigidBody;
    private SimpleTimer poolTimer;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    public void Apply()
    {
        Quaternion rotation = transform.rotation;
        Vector3 vector = Vector3.forward * 0.15f + Vector3.up + Vector3.right * 1.2f;
        vector *= 2.2f;
        vector = rotation * vector;
        //vector += EnemyHelper.Instance.GetPlayer.GetRigidBody.linearVelocity;
        rigidBody.linearVelocity = vector;
        poolTimer.Set(0.5f);
    }
    private void Update()
    {
        float t = Time.deltaTime * 600;
        transform.Rotate(t * 0.4f, t * 1f, t * 1f);
        if (poolTimer.IsTimerOver())
        {
            MonoGenericPool<BulletModel>.Push(this);
        }
    }
}
