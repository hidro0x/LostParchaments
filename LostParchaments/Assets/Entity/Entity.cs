using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Entity : MonoBehaviour, ITargetable, IDamageable
{
    public string Name;
    public EntityType Type;
    [SerializeField] private Stats stats;

    public Transform spellCastingPoint;
    public Stats Stats => stats;
    private Info _info;
    [SerializeField] private bool isTargetable;
    
    public EventChannelVoid OnHitChannel;
    
    public virtual void Awake()
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

    protected abstract void OnDie();

    public Info GetInfo()
    {
        if (!isTargetable) return null;
        _info = new Info(Name, stats.CurrHealth, transform);

        return _info;
    }

    public virtual void OnHit(float damageAmount)
    {
        stats.ReduceHealth(damageAmount);
        OnHitChannel.OnEventRaised?.Invoke();
    }
}

[System.Serializable]
public enum EntityType
{
    PLAYER,
    SKELETON,
}


