using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private MainCharacter _mainCharacter;
    [SerializeField]private SpellSlot[] spellSlots;

    public TextMeshProUGUI healthText, manaText;

    public Image healthBar, manaBar;
    public SpellSlot SelectedSpellSlot { get; private set; }

    public EventChannelVoid OnHitChannel;
    private void Awake()
    {
        _mainCharacter = GameObject.Find("Player").GetComponent<MainCharacter>();

    }

    private void Start()
    {
        RefreshHUD();
    }

    private void OnEnable()
    {
        StarterAssetsInputs.OnSpellChanged += ChangeSpell;
        StarterAssetsInputs.OnSpellThrowed += ThrowSpell;
        OnHitChannel.OnEventRaised += RefreshHUD;
    }

    private void OnDisable()
    {
        StarterAssetsInputs.OnSpellChanged -= ChangeSpell;
        StarterAssetsInputs.OnSpellThrowed -= ThrowSpell;
        OnHitChannel.OnEventRaised -= RefreshHUD;
    }
    
    private void ThrowSpell()
    {
        if(SelectedSpellSlot == null && TargetSelector.Instance.targetInfo.Transform == null) return;
        SelectedSpellSlot.Spell.CastSpell(_mainCharacter, TargetSelector.Instance.targetInfo.Transform);
        RefreshHUD();
        
    }

    private void ChangeSpell(int i)
    {
        if (SelectedSpellSlot != null) SelectedSpellSlot.UnselectSlot();
        SelectedSpellSlot = spellSlots[i - 1].SelectSlot();
    }

    private void RefreshHUD()
    {
        var stats = _mainCharacter.Stats;
        healthBar.fillAmount = stats.CurrHealth / stats.MaxHealth;
        manaBar.fillAmount = stats.CurrMana / stats.MaxMana;

        healthText.text = stats.CurrHealth.ToString("F0");
        manaText.text = stats.CurrMana.ToString("F0");
    }
}
