using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    private int healthAmount;
    [SerializeField] private int healthAmountMax;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void TakeDamage(int damageAmount)
    {
        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDead()
    {
        return healthAmount == 0;
    }

    public int GetHealthAmount()
    {
        return this.healthAmount;
    }

    public bool IsFullHealth()
    {
        return healthAmount == healthAmountMax;
    }

    public float GetHealthAmountNormalized()
    {
        return (float) healthAmount / healthAmountMax;
    }

    public void SetHealthAmountMax(int healthAmountMax, bool isUpdateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;

        if(isUpdateHealthAmount)
        {
            this.healthAmount = this.healthAmountMax;
        }
    }
}