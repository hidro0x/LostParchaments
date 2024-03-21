using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public string Name;
    private List<SpellSO> _spells;

    public Stats Stats { get; private set; }

    private void Awake()
    {
        Stats = GetComponent<Stats>();

        
    }

}
