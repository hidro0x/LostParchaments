using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacter : Entity
{
    protected override void OnDie()
    {
        var temp = SceneManager.GetActiveScene();
        SceneManager.LoadScene(temp.name);

    }

}
