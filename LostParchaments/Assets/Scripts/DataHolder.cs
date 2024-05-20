using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    private Dictionary<string, int> _killedEntities;

    public static DataHolder Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    

    public void AddToData_Enemy(EntityType entity)
    {
        if (_killedEntities.ContainsKey(entity.ToString()))
        {
            _killedEntities[entity.ToString()] += 1;
        }else _killedEntities.Add(entity.ToString(), 1);
    }
    
    public int GetData_Enemy(EntityType entity)
    {
        return _killedEntities.ContainsKey(entity.ToString()) ? _killedEntities[entity.ToString()] : 0;
    }
}
