using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private float durability;

    private Info _info;
    public void OnHit(float damageAmount)
    {
        if (damageAmount >= durability)
        {
            //Obje kırılsın.
            Destroy(gameObject);
        }
        else durability -= damageAmount;
    }

    
}
