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

    public LayerMask layerMask;


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

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _mainCam.transform.rotation * Vector3.forward, _mainCam.transform.rotation * Vector3.up);
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);
            if (hit.transform.TryGetComponent(out ITargetable targetable))
            {
                CurrentTarget = targetable;
                SetUI(CurrentTarget);
            }
            else
            {
                _panel.enabled = false;
                CurrentTarget = null;
                _targetInfo = null;
            }

        }
    }

    void SetUI(ITargetable targetable)
    {
        _targetInfo = targetable?.GetInfo();
        
        if (_targetInfo != null)
        {
            _panel.enabled = true;
            
            _panel.transform.SetParent(_targetInfo.Transform);
            _panel.transform.localPosition = new Vector3(0,1.5f,0);
            nameText.text = _targetInfo.Name;
            RefreshUI();
        }

    }
}
