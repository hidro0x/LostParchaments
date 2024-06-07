using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StartScreenController : MonoBehaviour
{
    public CinemachineVirtualCamera entryCam;
    public CinemachineVirtualCamera charCam;

    private Canvas _canvas;
    [SerializeField] private Canvas playerHud;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _canvas.enabled = false;
            playerHud.enabled = true;
            charCam.Priority = 10;
            entryCam.Priority = 8;
        }
    }
}
