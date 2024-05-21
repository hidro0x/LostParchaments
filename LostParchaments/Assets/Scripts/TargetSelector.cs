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

    public LayerMask targetLayer;


    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthAmountText;

    private ITargetable CurrentTarget;
    public Info targetInfo{ get; private set; }

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
        if (targetInfo != null)
        {
            RefreshUI();
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _mainCam.transform.rotation * Vector3.forward, _mainCam.transform.rotation * Vector3.up);
    }

    private void RefreshUI()
    {
        healthAmountText.text = targetInfo.Health.ToString("F0");
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, targetLayer))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.TryGetComponent(out ITargetable targetable))
            {
                CurrentTarget = targetable;
                SetUI(CurrentTarget);
            }
            else
            {
                _panel.enabled = false;
                CurrentTarget = null;
                targetInfo = null;
            }

        }
    }
    public void ResetPanel() => _panel.transform.SetParent(null);

    void SetUI(ITargetable targetable)
    {
        targetInfo = targetable.GetInfo();
        
        if (targetInfo != null)
        {
            _panel.enabled = true;
            
            _panel.transform.SetParent(targetInfo.Transform);
            _panel.transform.localPosition = new Vector3(0, 1.5f, 0);
            _panel.transform.localScale = Vector3.one;
            nameText.text = targetInfo.Name;
            RefreshUI();
        }

    }
}
