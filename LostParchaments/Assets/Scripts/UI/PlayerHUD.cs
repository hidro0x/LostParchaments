using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private MainCharacter _mainCharacter;
    [SerializeField]private SpellSlot[] spellSlots;
    public SpellSlot SelectedSpellSlot { get; private set; }
    

    private void Awake()
    {
        _mainCharacter = GameObject.Find("Player").GetComponent<MainCharacter>();

    }

    private void OnEnable()
    {
        StarterAssetsInputs.OnSpellChanged += ChangeSpell;
        StarterAssetsInputs.OnSpellThrowed += ThrowSpell;
    }

    private void OnDisable()
    {
        StarterAssetsInputs.OnSpellChanged -= ChangeSpell;
        StarterAssetsInputs.OnSpellThrowed -= ThrowSpell;
    }
    
    private void ThrowSpell()
    {
        if(SelectedSpellSlot == null && TargetSelector.Instance._targetInfo.Transform == null) return;
        SelectedSpellSlot.Spell.CastSpell(_mainCharacter, TargetSelector.Instance._targetInfo.Transform);
    }

    private void ChangeSpell(int i)
    {
        if (SelectedSpellSlot != null) SelectedSpellSlot.UnselectSlot();
        SelectedSpellSlot = spellSlots[i - 1].SelectSlot();
    }

    private void RefreshHUD()
    {
        
    }
}
