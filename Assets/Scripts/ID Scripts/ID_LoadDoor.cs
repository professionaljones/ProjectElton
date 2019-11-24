﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ID_LoadDoor : MonoBehaviour
{
    private LoadingDoorScript GetDoorScript;
    public MeshRenderer myRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        GetDoorScript = GetComponentInParent<LoadingDoorScript>();
        myRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.collider.gameObject)
        {
            if(other.collider.gameObject.GetComponent<ID_WeaponVFX>())
            {
                Debug.Log("Hit with player weapon");

            }
        }    
    }

}
