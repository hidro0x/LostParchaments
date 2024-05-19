using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    public Info GetInfo();
}

[System.Serializable]
public class Info
{
    public string Name;
    public Stats Stats;
    public Transform Transform;
    public Info(string name, Stats stats, Transform transform)
    {
        Name = name;
        Stats = stats;
        Transform = transform;
    }
    
}
