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
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
            {
                entity.transform.DOLookAt(hit.point, 0.3f, AxisConstraint.Y).OnComplete(delegate
                {
                    GameObject spell = Instantiate(spellPrefab, entity.spellCastingPoint.position, Quaternion.identity);
                    spell.GetComponent<ICastable>().Cast(hit.point);
                    _cooldownTimer = Time.time + cooldown;
                });
                
            }
        }

    }
    
    

}

