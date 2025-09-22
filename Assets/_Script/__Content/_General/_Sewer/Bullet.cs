using Custom.Pool;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform visual;
    private TrailRenderer bulletTrail;

    private float Radius => visual.transform.localScale.x * 0.5f;
    private float Length => visual.transform.localScale.z;

    private SimpleTimer deadTimer;
    private bool isInPool;
    private bool isPopInThisFrame;

    private const float k_bulletDeadTimer = 3;
    private const float k_speed = 145;

    private void Awake()
    {
        deadTimer.Set(k_bulletDeadTimer);
        bulletTrail = GetComponentInChildren<TrailRenderer>();
    }
    private void FixedUpdate()
    {
        Vector3 lastPoint = transform.position;
        if (!isInPool && deadTimer.IsTimerOver())
        {
            PushToPool();
        }

        //Color lastPC = Color.blue;
        if (isPopInThisFrame)
        {
            //lastPC = Color.cyan;
            lastPoint = transform.position;
            isPopInThisFrame = false;
        }

        float speed = k_speed * Time.fixedDeltaTime;

        Vector3 movementDirection = transform.forward;
        Vector3 movementVector = movementDirection * speed;
        transform.position += movementVector;

        float radius = Radius;
        //Vector3 endPoint = transform.position + movementDirection * (Length - radius);

        Vector3 lastPointResult = lastPoint - movementDirection * radius;
        //Debug.DrawRay(lastPointResult, Vector3.up, lastPC, 1);
        bool isHit = Physics.SphereCast(lastPointResult, radius, movementVector, out RaycastHit hitInfo, movementVector.magnitude, layerMask, QueryTriggerInteraction.Ignore);

        //Debug.DrawRay(lastPointResult, movementVector, Color.red, 1);

        bool destroy = isHit && !isInPool;
        if (destroy)
        {
            PushToPool();
            /*PoolGameObjectSO bulletImpactParticle = GameObjectPoolDefine.Instance.GetPoolGameObjectDictionary(PoolGameObjectType.Particle)[(int)ParticleType.Metal];
            GameObject bulletParticle = GameObjectPoolManager.Pop(bulletImpactParticle);
            Vector3 particleSpawnPosition = hitInfo.point;
            Quaternion particleSpawnRotation = Quaternion.LookRotation(hitInfo.normal, Vector3.up);
            bulletParticle.transform.SetPositionAndRotation(particleSpawnPosition, particleSpawnRotation);*/

            /*if (hitInfo.transform.TryGetComponent(out DamageListener damageListener))
            {
                DamageArgument damageArgument = new DamageArgument();
                damageArgument.damage = 5;
                damageArgument.multiplier = 1;
                damageListener.ApplyDamage(damageArgument);
            }*/
        }
        //lastPoint = endPoint;
    }
    void IPoolable.OnPop()
    {
        deadTimer.Set(k_bulletDeadTimer);
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
        MonoGenericPool<Bullet>.Push(this);
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 startPoint = transform.position - transform.forward * Radius;
        Vector3 endPoint = transform.position + transform.forward * Length;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPoint, Radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(endPoint, Radius);
    }
}
