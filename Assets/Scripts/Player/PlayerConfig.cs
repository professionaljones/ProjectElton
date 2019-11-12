﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

// [System.Serializable]
// public class MyFloatEvent : UnityEvent<float>
// {
// }

public class PlayerConfig : MonoBehaviour
{
    public float currentMouseSensitivity;
    public Slider mouseSlider;
    private MainMenuController menuController;

    //UnityEvent<float> myEvent;

    void Start()
    {
        menuController = FindObjectOfType<MainMenuController>();
       mouseSlider = FindObjectOfType<ID_MouseSlider>().mySlider;
       menuController.OptionsMenu.gameObject.SetActive(false);
	}

    void Update()
    {
        if(!mouseSlider)
        {
            
        }
        currentMouseSensitivity = mouseSlider.value;    
    }
    

    public void MouseSensitivityCheck()
    {
        mouseSlider.value = currentMouseSensitivity;
        // currentMouseSensitivity = newValue;
        // return newValue;
    }
}
