using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEventFire : MonoBehaviour
{
    [SerializeField] private Quest quest;
    private bool _isTriggered;
    private void Awake()
    {
        quest = Instantiate(quest);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isTriggered)
        {
            QuestManager.OnQuestStarted?.Invoke(quest);
            _isTriggered = true;
        }
    }
}
