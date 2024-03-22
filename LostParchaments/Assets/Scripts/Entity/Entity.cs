using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string Name;
    [SerializeField] private Stats stats;

    public Transform spellCastingPoint;
    public Stats Stats => stats;
}
