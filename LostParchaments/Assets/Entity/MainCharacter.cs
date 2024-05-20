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

}
