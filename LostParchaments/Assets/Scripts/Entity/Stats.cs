using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats : IDamageable
{
    [SerializeField] private int baseHealth, baseMana;
    private float _currHealth;
    private float _currMana;

    public float CurrHealth => _currHealth;
    public float CurrMana => _currMana;
    

    public void Init()
    {
        _currHealth = baseHealth;
        _currMana = baseMana;
    }

    public void ReduceMana(float amount)
    {
        if(amount > _currMana) return;
        _currMana -= amount;
    }
    
    private void ReduceHealth(float amount)
    {
        if(amount > _currHealth) return;
        _currHealth -= amount;
    }

    public void OnHit(float damageAmount)
    {
        ReduceHealth(damageAmount);
    }
}
