using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Create Spell")]
public class SpellSO : ScriptableObject
{
    public string Name;
    [SerializeField] int manaCost;
    [SerializeField] private GameObject spellPrefab;

    public bool IsCastable(Stats stats) { return stats.CurrMana >= manaCost; }

    public void CastSpell(Stats stats, Transform castingPoint, Transform target)
    {
        if(!IsCastable(stats)) return;
        stats.ReduceMana(manaCost);
        Instantiate(spellPrefab, castingPoint);
    }

}

