using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    public Info GetInfo();
}

public class Info
{
    public string Name;
    public float Health;
    public Transform Transform;
    public Info(string name, float health, Transform transform)
    {
        Name = name;
        Health = health;
        Transform = transform;
    }
    
}
