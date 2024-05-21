using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera dialogueCam;

    [SerializeField] private Canvas textCanvas;
    [SerializeField] private Canvas playerHudCanvas;
    [SerializeField] private TextMeshProUGUI header, desc;

    [Space] [SerializeField] private Button cancelBtn, confirmBtn;
    
    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        cancelBtn.onClick.AddListener(CloseUI);

    }

    public void OpenDialogue(InteractableNPC npc, Quest quest)
    {
        if(quest == null) return;
        SetCam(npc.transform);
        SetUI(quest);
    }

    void SetUI(Quest quest)
    {
        header.text = quest.Name;
        desc.text = quest.Dialogue;
        
        confirmBtn.onClick.RemoveAllListeners();
        confirmBtn.onClick.AddListener(() => QuestManager.OnQuestStarted(quest));
        confirmBtn.onClick.AddListener(CloseUI);

        textCanvas.enabled = true;
        playerHudCanvas.enabled = false;

    }

    void CloseUI()
    {
        textCanvas.enabled = false;
        playerHudCanvas.enabled = true;
        
        OffCam();
    }

    void SetCam(Transform pos)
    {
        dialogueCam.LookAt = pos;
        dialogueCam.Priority = 11;
    }

    void OffCam() => dialogueCam.Priority = 9;

}
