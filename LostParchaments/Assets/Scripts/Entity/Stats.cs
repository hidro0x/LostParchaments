using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField] private int baseHealth, baseMana;
    private float _currHealth;
    private float _currMana;

    public Action OnHealthRunsOut;

    public float CurrHealth => _currHealth;
    public float CurrMana => _currMana;

    public int MaxHealth => baseHealth;
    public int MaxMana => baseMana;
    

    public void Init()
    {
        _currHealth = baseHealth;
        _currMana = baseMana;
    }

    public void GiveHealth(float amount)
    {
        if (amount + _currHealth > MaxHealth)
        {
            _currHealth = MaxHealth;
            return;
        }

        _currHealth += amount;
    }
    
    public void GiveMana(float amount)
    {
        if (amount + _currMana > MaxMana)
        {
            _currMana = MaxMana;
            return;
        }

        _currMana += amount;
    }

    public void ReduceMana(float amount)
    {
        if(amount > _currMana) return;
        _currMana -= amount;
    }
    
    public void ReduceHealth(float amount)
    {
        if (amount >= _currHealth)
        {
            OnHealthRunsOut?.Invoke();
            return;
        }
        _currHealth -= amount;
    }
    
}
