using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Entity
{
    protected override void OnDie()
    {
        Time.timeScale = 0;
    }

    public override void OnHit(float damageAmount)
    {
        base.OnHit(damageAmount);
        
    }
}
