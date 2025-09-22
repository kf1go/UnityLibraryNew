using System;
using UnityEngine;

public class Health2 : MonoBehaviour
{
    public event Action<DamageArgument> OnDamaged;
    /// <summary>
    /// dont remove event 
    /// event's add, remove keyword is necessary to make this evil function work
    /// <see cref="WaveManager.SpawnEnemy(int)"/>
    /// /// </summary>
    public event Action OnDead;
    public event Action OnHealthChange;

    public float HealthValue
    {
        get => healthValue;
        set
        {
            healthValue = value;
            if (healthValue > maxHealthValue)
            {
                healthValue = maxHealthValue;
            }

            if (OnHealthChange != null)
            {
                OnHealthChange.Invoke();
            }
        }
    }
    [SerializeField] private float healthValue;

    public float MaxHealthValue
    {
        get => maxHealthValue;
        set
        {
            maxHealthValue = value;
            Debug.Assert(healthValue <= MaxHealthValue);
        }
    }
    [SerializeField] private float maxHealthValue;

    public bool IsDead => healthValue < 1;

    private void Awake()
    {
        Debug.Assert(healthValue <= MaxHealthValue);
    }
    public void Damage(DamageArgument argument)
    {
        if (IsDead)
        {
            return;
        }

        HealthValue -= argument.ResultDamage;

        if (OnDamaged != null)
        {
            OnDamaged.Invoke(argument);
        }

        if (IsDead)
        {
            if (OnDead != null)
            {
                OnDead.Invoke();
            }
        }
    }
    private void OnDestroy()
    {
        OnDamaged = null;
        OnDead = null;
    }
}
