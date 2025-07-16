using UnityEngine;

namespace Custom.Pool
{
    public abstract class BasePoolObjectSO : ScriptableObject
    {
        [field: SerializeField] public int PrewarmCount { get; private set; } // TODO : not used
        [field: SerializeField] public int InitialPoolCapacity { get; private set; } = 16;
        [field: SerializeField] public int MaxCapacity { get; private set; } = 1000;

        [field: SerializeField] public GameObject Prefab { get ; private set; }
        [field: SerializeField] public int Hash { get; private set; } //serialized for data saving, readonly
        protected virtual void OnValidate()
        {
            Hash = Prefab.GetHashCode(); // TODO : is this fine?
        }
    }
}
