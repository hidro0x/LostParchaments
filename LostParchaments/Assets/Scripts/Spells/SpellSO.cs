using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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
        entity.transform.rotation = Quaternion.LookRotation(-entity.transform.position + target.transform.position);
        stats.ReduceMana(manaCost);
        var spell =Instantiate(spellPrefab, entity.spellCastingPoint);
        spell.transform.SetParent(null);
        _cooldownTimer = Time.time + cooldown;
    }

}

