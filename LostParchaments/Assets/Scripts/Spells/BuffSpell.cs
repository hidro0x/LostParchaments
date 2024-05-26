using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpell : MonoBehaviour, ICastable
{
    [SerializeField] private int healthAmount;
    [SerializeField] private int manaAmount;
    public GameObject healEffect;
    
    
    public void Cast(Entity entity,Vector3 point)
    {
        entity.Stats.GiveHealth(healthAmount);
        entity.Stats.GiveMana(manaAmount);
        var temp = Instantiate(healEffect, transform.position, Quaternion.identity);
        Destroy(temp, 2f);
    }
}
