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

    public float viewRange;

    GameObject PlayerCharacter;
    Camera PlayerCamera;
    Vector2 MouseDirection;

    void Awake()
    {
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
        ///Commenting this line causes right angle turns, almost like Time Crisis
        PlayerCharacter.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, PlayerCharacter.transform.up);
    }

    private void LateUpdate()
    {
        MouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        
        PlayerCamera.transform.localEulerAngles = new Vector3(MouseDirection.x,PlayerCamera.transform.localEulerAngles.y,PlayerCamera.transform.localEulerAngles.z);
        
        MouseDirection = Vector2.Scale(MouseDirection, new Vector2(LookSensitivity * SmoothingRate, LookSensitivity * SmoothingRate));
        smoothingVector.x = Mathf.Lerp(smoothingVector.x, MouseDirection.x, 1f / SmoothingRate);
        smoothingVector.y = Mathf.Lerp(smoothingVector.y, MouseDirection.y, 1f / SmoothingRate);
        mouseLook += smoothingVector;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        
    }
}
