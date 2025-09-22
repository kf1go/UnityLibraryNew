using Custom.Pool;
using UnityEngine;

public class ParticleGameObjectPoolInstance : MonoBehaviour
{
    [SerializeField] private PoolGameObjectSO prefabSO;

    private void OnParticleSystemStopped()
    {
        GameObjectPoolManager.Push(prefabSO, gameObject);
    }
}
