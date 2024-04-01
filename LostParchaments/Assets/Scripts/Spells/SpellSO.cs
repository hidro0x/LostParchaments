using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Spells/Create Spell")]
public class SpellSO : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    [SerializeField] private float cooldown;
    private float _cooldownTimer;
    [SerializeField] int manaCost;
    [SerializeField] private GameObject spellPrefab;

    public float Cooldown => cooldown;
    public float CooldownTimer => _cooldownTimer;
    public bool isOnCooldown => Time.time < _cooldownTimer;
    public bool IsCastable(Stats stats)
    {
        return stats.CurrMana >= manaCost && !isOnCooldown;
    }

    public void CastSpell(Entity entity, Transform target)
    {
        var stats = entity.Stats;
        if(!IsCastable(stats)) return;
        entity.transform.DOLookAt(target.position, 0.2f, AxisConstraint.Y).OnComplete(delegate
        {
            stats.ReduceMana(manaCost);
            var spell =Instantiate(spellPrefab, entity.spellCastingPoint);
            spell.transform.SetParent(null);
            _cooldownTimer = Time.time + cooldown;
        });
        
    }

}

