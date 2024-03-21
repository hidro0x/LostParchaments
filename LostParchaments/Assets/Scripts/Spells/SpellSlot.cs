using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellSlot : MonoBehaviour
{
    public Image spellIcon;
    public Image background;
    public Image cooldownPanel;

    public TextMeshProUGUI cooldownText;
        
    private SpellSO _spell;
    private bool _isSlotSelected;

    public SpellSO Spell => _spell;
    


    private void InitSlot(SpellSO spell)
    {
        _spell = spell;
        spellIcon.sprite = _spell.Icon;
        
    }
    
    private void RefreshUI()
    {
        background.color = _isSlotSelected ? Color.white : Color.yellow;
    }

    public void SelectSlot() => _isSlotSelected = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
