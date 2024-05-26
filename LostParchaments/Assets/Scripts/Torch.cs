using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public bool isBurning;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !isBurning)
        {
            isBurning = true;
            QuestManager.UpdateQuestProgress?.Invoke(QuestType.PUZZLE);
        }
    }
}