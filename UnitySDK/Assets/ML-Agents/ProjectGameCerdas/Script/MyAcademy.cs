using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAcademy : Academy
{
    //private myArea[] areas;
    [Header("Specific to Shooting")]
    public float agentRunSpeed;
    public float agentJumpHeight;
    //when a goal is scored the ground will use this material for a few seconds.
    public Material successMaterial;
    //when fail, the ground will use this material for a few seconds. 
    public Material failMaterial;
    public float gravityMultiplier = 2.5f;

    public override void InitializeAcademy()
    {
        Physics.gravity *= gravityMultiplier;
    }
}
