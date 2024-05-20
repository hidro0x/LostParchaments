using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    public string Name;
    [SerializeField] List<Quest> quests;

    private Canvas _textBox;


    public void Interact()
    {
        DialogueManager.Instance.OpenDialogue(this, SelectQuest());
    }

    private Quest SelectQuest()
    {
        foreach (var quest in quests)
        {
            if (quest.State == QuestStates.QUEST_AVAILABLE) return quest;
        }

        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _textBox.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !_textBox.isActiveAndEnabled)
        {
            Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _textBox.enabled = false;
        }
    }
}
