using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

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

    public void CastSpell(Entity entity)
    {
        var stats = entity.Stats;
        if(!IsCastable(stats)) return;
        stats.ReduceMana(manaCost);
        RaycastHit hit;
        if (!EventSystem.current.IsPointerOverGameObject()) //Checks if the mouse is not over a UI part
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f)) //Finds the point where you click with the mouse
            {
                GameObject spell = Instantiate(spellPrefab, entity.spellCastingPoint.position, Quaternion.identity); //Spawns the selected project
                spell.GetComponent<ICastable>().Cast(hit.point);
                _cooldownTimer = Time.time + cooldown;
            }
        }

    }

}

