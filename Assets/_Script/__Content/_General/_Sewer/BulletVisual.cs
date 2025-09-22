using Custom.Pool;
using UnityEngine;

/// <summary>
/// fake bullet
/// </summary>
public class BulletVisual : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform visual;
    private TrailRenderer bulletTrail;

    private float Radius => visual.transform.localScale.x * 0.5f;

    private SimpleTimer deadTimer;
    private bool isInPool;
    private bool isPopInThisFrame;
    private const float k_speed = 200;

    private void Awake()
    {
        bulletTrail = GetComponentInChildren<TrailRenderer>();
    }
    private void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, Vector3.up, Color.magenta, 0.5f);
        //Vector3 lastPoint = transform.position;
        if (!isInPool && deadTimer.IsTimerOver())
        {
            PushToPool();
        }

        //Color lastPC = Color.blue;
        if (isPopInThisFrame)
        {
            //lastPC = Color.cyan;
            //lastPoint = transform.position;
            isPopInThisFrame = false;
        }

        float speed = k_speed * Time.fixedDeltaTime;

        Vector3 movementDirection = transform.forward;
        Vector3 movementVector = movementDirection * speed;
        transform.position += movementVector;
    }
    public void Fuck()
    {
        bool isHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, float.MaxValue, layerMask, QueryTriggerInteraction.Ignore);
        float time = isHit ? hitInfo.distance / k_speed : 3;
        //Debug.Log(hitInfo.distance);
        deadTimer.Set(time);
    }
    void IPoolable.OnPop()
    {
        deadTimer.Set(1);//fuk
        isInPool = false;
        isPopInThisFrame = true;
        bulletTrail.emitting = false;
        bulletTrail.Clear();
        bulletTrail.emitting = true;
    }
    private void PushToPool()
    {
        Debug.Assert(!isInPool, "already in pool");
        isInPool = true;
        MonoGenericPool<BulletVisual>.Push(this);
    }
}
