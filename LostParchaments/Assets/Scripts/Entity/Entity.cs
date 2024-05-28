using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    public string Name;
    public EntityType Type;
    [SerializeField] private Stats stats;

    public Transform spellCastingPoint;
    public Stats Stats => stats;
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
    ORC,
    BAT,
    SLIME,
}


