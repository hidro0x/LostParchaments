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
    public Info _targetInfo{ get; private set; }

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

    private void Update()
    {
        if (_targetInfo != null)
        {
            RefreshUI();
        }
    }

    private void RefreshUI()
    {
        healthAmountText.text = _targetInfo.Health.ToString("F0");
    }

    private void OnEnable()
    {
        StarterAssetsInputs.OnMouseClicked += Check;
    }

    private void OnDisable()
    {
        StarterAssetsInputs.OnMouseClicked -= Check;
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
        _targetInfo = targetable?.GetInfo();
        
        if (_targetInfo != null)
        {
            _panel.enabled = true;
            
            _panel.transform.SetParent(transform);
            _panel.transform.localPosition = new Vector3(0,1.5f,0);
            
            nameText.text = _targetInfo.Name;
            RefreshUI();
        }
        else _panel.enabled = false;

    }
}
