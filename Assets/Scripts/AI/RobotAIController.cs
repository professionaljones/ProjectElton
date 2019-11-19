﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotAIController : AIControllerBase
{

    private Animator myAnimator;

    private void Awake() 
    {
        base.Awake(); 
        bIsHumanoid = true;   
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponentInParent<Animator>();
        myAnimator.SetFloat("MovementSpeed", 0.85f);
    }

    // Update is called once per frame
    void Update()
    {
        VisionRaycast();
        if (myNavAgent.myNavPoints != null)
        {
            DistanceRemaining = myNavMeshAgent.remainingDistance;

            if (myNavMeshAgent.remainingDistance <= 5.0f)
            {
                myAnimator.SetFloat("MovementSpeed", 0.5f);
                // Debug.Log("We're getting close to the target!" + " says: " + gameObject.name);

            }
            else
            {
                myAnimator.SetFloat("MovementSpeed", 0.85f);
            }
        }

    }
}
