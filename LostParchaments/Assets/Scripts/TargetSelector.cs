using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TargetSelector : MonoBehaviour
{
    private Camera _mainCam;

    private Canvas _panel;
    
    public Image healthBarFill;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthAmountText;

    private ITargetable CurrentTarget;
    public Info targetInfo{ get; private set; }
    
    public EventChannelVoid OnHitChannel;

    public static TargetSelector Instance;
    


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        
        
        _mainCam = Camera.main;
        _panel = GetComponent<Canvas>();
    }
    
    

    private void RefreshUI()
    {
        healthAmountText.text = targetInfo.Stats.CurrHealth.ToString("F0");
        healthBarFill.fillAmount = targetInfo.Stats.CurrHealth / targetInfo.Stats.MaxHealth;
    }

    private void OnEnable()
    {
        StarterAssetsInputs.OnMouseClicked += Check;
        OnHitChannel.OnEventRaised += RefreshUI;
    }

    private void OnDisable()
    {
        StarterAssetsInputs.OnMouseClicked -= Check;
        OnHitChannel.OnEventRaised -= RefreshUI;
    }

    void Check()
    {
        RaycastHit hit;
        Ray ray = _mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit))
        {
            CurrentTarget = hit.transform.TryGetComponent(out ITargetable targetable) ? targetable : null;
            SetUI(CurrentTarget, hit.transform);
        }
    }

    void SetUI(ITargetable targetable, Transform transform)
    {
        targetInfo = targetable?.GetInfo();
        
        if (targetInfo != null)
        {
            _panel.enabled = true;
            
            _panel.transform.SetParent(transform);
            _panel.transform.localPosition = new Vector3(0,1.5f,0);
            
            nameText.text = targetInfo.Name;
            RefreshUI();
        }
        else _panel.enabled = false;
    }
    
}
