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
        
    [SerializeField]private SpellSO attachedSpell;
    private SpellSO _spell;
    private bool _isSlotSelected;

    public SpellSO Spell => _spell;

    private void Start()
    {
        InitSlot();
    }

    private void InitSlot()
    {
        _spell = Instantiate(attachedSpell);
        spellIcon.sprite = _spell.Icon;
        cooldownPanel.fillAmount = 0f;
    }
    
    private void RefreshUI()
    {
        background.color = _isSlotSelected ? Color.yellow : Color.black;
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
        if (_spell.isOnCooldown)
        {
            var leftTime = _spell.CooldownTimer - Time.time;
            cooldownPanel.fillAmount = leftTime / _spell.Cooldown;
            cooldownText.text = leftTime.ToString("F1") + "s";
        }
        else
        {
            cooldownText.text = " ";
        }
    }
}
