using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private Canvas questCanvas;
    [SerializeField] private TextMeshProUGUI header, desc, progress;

    public void SetUI(Quest quest)
    {
        questCanvas.enabled = true;
        RefreshUI(quest);
    }

    public void RefreshUI(Quest quest)
    {
        header.text = quest.Name;
        desc.text = quest.Description;
        progress.text = quest.Progress();

    }

    public void CompleteUI()
    {
        questCanvas.enabled = false;
    }
}
