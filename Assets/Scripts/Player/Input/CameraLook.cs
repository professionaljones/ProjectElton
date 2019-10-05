﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    Vector2 mouseLook;
    //Ensures smooth motion
    Vector2 smoothingVector;
    //How sensitive should the change be
    public float LookSensitivity = 5.0f;
    public float SmoothingRate = 2.0f;

    private GameObject PlayerCharacter;
    private PlayerController pCon;
    private Camera PlayerCamera;
    private bool bStartLockOn = false;
    Vector2 MouseDirection;
    Vector2 GamepadDirection;

    public RaycastHit LockOnHit;
    public float lockOnRange = 200f;
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
    Ray lockOnRay;
    private Enemy currentEnemy;

    void Awake()
    {
        pCon = GetComponentInParent<PlayerController>();
        PlayerCamera = Camera.main;
        PlayerCharacter = FindObjectOfType<Player>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pCon.bEnableInput)
        {
            ///Commenting this line causes right angle turns, almost like Time Crisis
            PlayerCharacter.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, PlayerCharacter.transform.up);
        }
        if (Input.GetButtonDown("AltAttack") || Input.GetButtonDown("GPAltAttack"))
        {
            bStartLockOn = true;
        }
        if (Input.GetButtonUp("AltAttack") || Input.GetButtonUp("GPAltAttack"))
        {
            bStartLockOn = false;
            if(currentEnemy)
            {
                currentEnemy.enemyUIController.bIsTargeted = false;
                currentEnemy = null;
            }
        }



    }

    private void LateUpdate()
    {
        if (pCon.bEnableInput)
        {
            //This section of code was originally done by someone(s) else, I cannot find where at this time, will update when found
            MouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            GamepadDirection = new Vector2(Input.GetAxisRaw("LookRight"), Input.GetAxisRaw("LookUp"));
            // if (pCon.bIsGamepad)
            // {
            PlayerCamera.transform.localEulerAngles = new Vector3(GamepadDirection.x, PlayerCamera.transform.localEulerAngles.y, PlayerCamera.transform.localEulerAngles.z);
            GamepadDirection = Vector2.Scale(GamepadDirection, new Vector2(LookSensitivity * SmoothingRate, LookSensitivity * SmoothingRate));
            smoothingVector.x = Mathf.Lerp(smoothingVector.x, GamepadDirection.x, 1f / SmoothingRate);
            smoothingVector.y = Mathf.Lerp(smoothingVector.y, GamepadDirection.y, 1f / SmoothingRate);
            mouseLook += smoothingVector;
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            // }
            //else
            //  {
            PlayerCamera.transform.localEulerAngles = new Vector3(MouseDirection.x, PlayerCamera.transform.localEulerAngles.y, PlayerCamera.transform.localEulerAngles.z);

            MouseDirection = Vector2.Scale(MouseDirection, new Vector2(LookSensitivity * SmoothingRate, LookSensitivity * SmoothingRate));
            smoothingVector.x = Mathf.Lerp(smoothingVector.x, MouseDirection.x, 1f / SmoothingRate);
            smoothingVector.y = Mathf.Lerp(smoothingVector.y, MouseDirection.y, 1f / SmoothingRate);
            mouseLook += smoothingVector;
             mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            //}
            LockOn();

        }
    }

    void LockOn()
    {
        lockOnRay = PlayerCamera.ViewportPointToRay(rayOrigin);
        Debug.DrawRay(lockOnRay.origin, lockOnRay.direction * lockOnRange, Color.white);
        if (bStartLockOn)
        {
            if (Physics.Raycast(lockOnRay, out LockOnHit, lockOnRange))
            {
                if (LockOnHit.collider)
                {
                    currentEnemy = LockOnHit.collider.gameObject.GetComponent<Enemy>();
                    if (currentEnemy)
                    {
                        transform.LookAt(currentEnemy.gameObject.transform);
                        currentEnemy.enemyUIController.bIsTargeted = true;
                    }
                }
            }
        }

    }
}
