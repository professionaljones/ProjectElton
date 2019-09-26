﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStateScript : MonoBehaviour
{
    public float CurrentStyleAmount = 0;

    public float MaxStyleAmount = 250;

    public float StyleDecAmount = 0.01f;
    public float StyleDecRate = 1.0f;

    public float DifficultyMod = 1.0f;

    public float StylePercent;
    public float StyleModAmount;
    private ID_StyleImage styleImageScript;
    private ID_StyleSlider styleSliderScript;
    // Start is called before the first frame update
    void Start()
    {
        styleImageScript = FindObjectOfType<ID_StyleImage>();
        styleSliderScript = FindObjectOfType<ID_StyleSlider>();
    }

    // Update is called once per frame
    void Update()
    {
        StylePercent = CurrentStyleAmount / MaxStyleAmount;
        if (CurrentStyleAmount > 0)
        {
            if (StylePercent > 0.80)
            {
                styleImageScript.styleImage.color = Color.magenta;
            }
            if (StylePercent > 0.60)
            {
                styleImageScript.styleImage.color = Color.cyan;
            }
            if (StylePercent > 0.40)
            {
                styleImageScript.styleImage.color = Color.blue;
            }
            if (StylePercent <= 0.25)
            {
                styleImageScript.styleImage.color = Color.red;
            }
            StartCoroutine(DecreaseStyleOverTime());
        }
        if (StylePercent <= 0)
        {
            styleImageScript.gameObject.SetActive(false);
            styleSliderScript.gameObject.SetActive(false);
        }
        else
        {
            styleImageScript.gameObject.SetActive(true);
            styleSliderScript.gameObject.SetActive(true);
        }

    }

    public void ModStyle(float ModAmount)
    {
        StyleModAmount = ModAmount * DifficultyMod;
        CurrentStyleAmount += StyleModAmount;
    }

    public void PlayerStyleDamageMod(float ModAmount)
    {
        StyleModAmount = ModAmount;
        CurrentStyleAmount -= StyleModAmount;
    }

    public IEnumerator DecreaseStyleOverTime()
    {
        CurrentStyleAmount -= StyleDecAmount;
        yield return StyleDecRate;
    }
}
