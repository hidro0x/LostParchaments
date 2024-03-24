using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class SpellSlot : MonoBehaviour
{
    public Image spellIcon;
    public Image background;
    public Image cooldownPanel;

    public TextMeshProUGUI cooldownText;
        
    [SerializeField]private SpellSO spell;
    private bool _isSlotSelected;

    public SpellSO Spell => spell;

    private void Start()
    {
        InitSlot();
    }

    private void InitSlot()
    {
        spellIcon.sprite = spell.Icon;
        cooldownPanel.fillAmount = 0f;
    }
    
    private void RefreshUI()
    {
        background.color = _isSlotSelected ? Color.yellow : Color.white;
    }

    public SpellSlot SelectSlot()
    {
        _isSlotSelected = true;
        RefreshUI();
        return this;
    }

    public void UnselectSlot()
    {
        _isSlotSelected = false;
        RefreshUI();
    }
    
    void Update()
    {
        if (spell.isOnCooldown)
        {
            var leftTime = spell.CooldownTimer - Time.time;
            cooldownPanel.fillAmount = leftTime / spell.Cooldown;
            cooldownText.text = leftTime.ToString("F1") + "s";
        }
        else
        {
            cooldownText.text = " ";
        }
    }
}
