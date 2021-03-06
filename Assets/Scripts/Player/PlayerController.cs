﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool bIsGamePaused = false;
    public bool bEnableInput = true;
    public bool bIsGamepad = false;
    public PlayerStateScript playerState;
    public RigidbodyCharacterMovement rbMovementScript;
    public CameraLook cameraScript;
    public GameObject PauseMenuGO;

    void Awake()
    {
        rbMovementScript = GetComponentInParent<RigidbodyCharacterMovement>();
        playerState = GetComponentInChildren<PlayerStateScript>();
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        PauseMenuGO = FindObjectOfType<ID_PauseMenu>().gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }

        if (PauseMenuGO == null)
        {
            PauseMenuGO = FindObjectOfType<ID_PauseMenu>().gameObject;
        }

        if (Input.GetKeyDown("joystick button 0"))
        {
            bIsGamepad = true;
            Debug.LogWarning("Now using Gamepad Input!");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            bIsGamepad = false;
            Debug.LogWarning("Now using KB Input!");
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }

            }
        }
    }

    public void PauseGame()
    {
        bIsGamePaused = !bIsGamePaused;
        if (bIsGamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            bEnableInput = false;
            if (PauseMenuGO)
            {
                if (!PauseMenuGO.activeSelf)
                {
                    PauseMenuGO.SetActive(true);
                }
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
            bEnableInput = true;
            if (PauseMenuGO)
            {
                if (PauseMenuGO.activeSelf)
                {
                    PauseMenuGO.SetActive(false);
                }
            }
        }

    }
}
