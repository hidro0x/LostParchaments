using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private MainCharacter _mainCharacter;
    [SerializeField]private SpellSlot[] spellSlots;

    private void Awake()
    {
        _mainCharacter = GameObject.Find("Player").GetComponent<MainCharacter>();

    }

    void OnSelectSpell(InputAction value)
    {
        value.ge
    }
}
