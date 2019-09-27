﻿using UnityEngine.SceneManagement;
using UnityEngine;

public class ID_PlayerUI : MonoBehaviour
{
    private static ID_PlayerUI instance = null;
    public static ID_PlayerUI Instance { get { return instance; } }
    void Start()
    {
         if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

        }
        DontDestroyOnLoad(this);
    }
}