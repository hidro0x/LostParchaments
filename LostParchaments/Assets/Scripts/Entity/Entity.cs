using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour, ITargetable, IDamageable
{
    public string Name;
    [SerializeField] private Stats stats;

    public Transform spellCastingPoint;
    public Stats Stats => stats;
    private Info _info;
    [SerializeField] private bool isTargetable;


    private void Awake()
    {
        stats.Init();
    }

    private void OnEnable()
    {
        stats.OnHealthRunsOut += OnDie;
    }

    private void OnDisable()
    {
        stats.OnHealthRunsOut -= OnDie;
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }

    public Info GetInfo()
    {
        if (!isTargetable) return null;
        
        if (_info == null)
        {
            _info = new Info(Name, stats.CurrHealth, transform);
        }
        else _info.Health = stats.CurrHealth;

        return _info;
    }

    public virtual void OnHit(float damageAmount)
    {
        stats.ReduceHealth(damageAmount);
    }
}


