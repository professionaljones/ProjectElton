﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    public enum ESpecialAbility
    {
        None,
        Dash,
        Regen,
        Booster,
    }
    private HealthTextScript healthText;
    public CharacterStats pcStats;
    private Player player;
    public ESpecialAbility currentSpecialAbility;
    private int styleIndex = 0;

    private bool bIsDebug = false;

    void Awake()
    {
        pcStats = GetComponent<CharacterStats>();
        pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Standard;
        player = GetComponent<Player>();
        healthText = FindObjectOfType<HealthTextScript>();

    }

    // Start is called before the first frame update
    void Start()
    {
        // healthText = FindObjectOfType<HealthTextScript>();
        UpdateHealthText();
        ModifyCurrentStyleUp();
        if (Debug.isDebugBuild || Application.isEditor)
        {
            bIsDebug = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            if (bIsDebug)
            {
                player.PlayerDamageTaken(10.0f);
            }
        }
        if (styleIndex <= 0)
        {
            styleIndex = 1;
        }
    }

    public void UpdateHealthText()
    {
        if (healthText)
        {
            healthText.TextMesh.text = pcStats.CurrentHealth.ToString();
        }
    }

    public void ModifyCurrentStyleUp()
    {
        Debug.Log(pcStats.currentCharacterStyle);
        styleIndex = styleIndex + 1;
        switch (styleIndex)
        {
            case 1:
                styleIndex = 1;
                pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Offense;
                break;
            case 2:
                styleIndex = 2;
                pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Defense;
                break;
            case 3:
                styleIndex = 3;
                pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Speed;
                break;
            case 4:
                styleIndex = 4;
                pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Regen;
                break;
            case 5:
                styleIndex = 0;
                //pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Offense;
                break;

        }





    }

    public void ModifyCurrentStyleDown()
    {
        styleIndex = styleIndex - 1;
        switch (styleIndex)
        {
            case 1:
                styleIndex = 1;
                pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Offense;
                break;
            case 2:
                styleIndex = 2;
                pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Defense;
                break;
            case 3:
                styleIndex = 3;
                pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Speed;
                break;
            case 4:
                styleIndex = 4;
                pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Regen;
                break;
            // case 5:
            //     styleIndex = 1;
            //     //pcStats.currentCharacterStyle = CharacterStats.ECharacterStyle.Offense;
            //     break;
        }
    }

}
