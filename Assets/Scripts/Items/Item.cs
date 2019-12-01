﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Item : MonoBehaviour
{
    public enum ItemType
    {
        HealthPickup,
        AragonPickup,
        SubweaponAmmo,
        PowerUp,
        HealthUpgrade,
        SubweaponAmmoUpgrade
    }
    public float ValueMod = 0;
    public Player GetPlayer;
    public AudioSource itemSource;

    public ItemType CurrentItemType;

    public void Awake()
    {
        GetPlayer = FindObjectOfType<Player>();
        itemSource = GetComponent<AudioSource>();
    }




    public void RecoverHealth()
    {
        if (CurrentItemType == ItemType.HealthPickup)
        {
            GetPlayer.SendMessage("HealCharacter", ValueMod);
            //GetPlayer.characterStats.HealCharacter(RecoverAmount);
            GetPlayer.PlayerStats.updateDataEvent.Invoke();
            //GetPlayer.PlayerStats.UpdateHealthText();
            itemSource.PlayOneShot(itemSource.clip);
        }

    }

    public void RecoverAragon()
    {
        if (CurrentItemType == ItemType.AragonPickup)
        {
            GetPlayer.PlayerStats.SendMessage("RecoverPower", ValueMod);
            GetPlayer.PlayerStats.updateDataEvent.Invoke();
            itemSource.PlayOneShot(itemSource.clip);
        }
    }

    public void RecoverAmmo()
    {
        if (CurrentItemType == ItemType.SubweaponAmmo)
        {
            GetPlayer.pCon.combatController.currentSubWeapon.SendMessage("ModifyAmmo", ValueMod);
            itemSource.PlayOneShot(itemSource.clip);
        }
    }

    public void UpgradeHealth()
    {
        if (CurrentItemType == ItemType.HealthUpgrade)
        {
            GetPlayer.SendMessage("ModifyHealth", ValueMod);
            GetPlayer.PlayerStats.updateDataEvent.Invoke();
            //SaveSystem.SavePlayer(GetPlayer);
            SaveManager.SavePlayerData();
        }
    }



}
