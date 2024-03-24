using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable, ITargetable
{
    [SerializeField] private float durability;

    private Info _info;
    public void OnHit(float damageAmount)
    {
        if (damageAmount >= durability)
        {
            //Obje kırılsın.
            Destroy(this);
        }
        else durability -= damageAmount;
    }


    public Info GetInfo()
    {
        if (_info == null)
        {
            _info = new Info(gameObject.name, durability, transform);
        }
        else
        {
            _info.Health = durability;
        }

        return _info;
    }
}
