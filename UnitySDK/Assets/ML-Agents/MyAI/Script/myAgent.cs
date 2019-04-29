using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
public class myAgent : Agent
{

    [Header("My Agent move")]
    public float rotateSpeed = 2f;
    public float moveSpeed = 1f;
    public float sideSpeed = 1f;
    private RayPerception rayPerception;
    private myAcademy agentAcademy;
    private myArea agentArea;
    private Rigidbody agentRigidbody;
    private Transform agentTransorm;

    public override void InitializeAgent()
    {
        base.InitializeAgent();
        agentAcademy = FindObjectOfType<myAcademy>();
        agentArea = transform.parent.GetComponent<myArea>();
        agentRigidbody = GetComponent<Rigidbody>();
        rayPerception = GetComponent<RayPerception>();
    }
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        AddReward(0.001f);
        //Debug.Log(GetCumulativeReward().ToString());
        //float rotateAmount = 0f;
        float moveAmount = 0f;
        float sideAmount= 0f;
        if (vectorAction[0] == 2)
        {
            //rotateAmount = -rotateSpeed;
            ////Debug.Log("mutar");
            sideAmount = sideSpeed;
        }
        else if(vectorAction[0] == 1)
        {
            sideAmount = -sideSpeed;
        }
        //Vector3 rotateVector = transform.up * rotateAmount;
        //agentRigidbody.MoveRotation(Quaternion.Euler(agentRigidbody.rotation.eulerAngles + rotateVector * rotateSpeed));
        // Determine side walk action
        else if (vectorAction[0] == 4)
        {
            moveAmount = -moveSpeed;
        }
        else if (vectorAction[0] == 3)
        {
            moveAmount = moveSpeed; // move at half-speed going backwards
        }

        // Apply the movement
        Vector3 sideVector = transform.right * sideAmount;
        agentRigidbody.AddForce(sideVector * sideSpeed, ForceMode.VelocityChange);

        Vector3 moveVector = transform.forward * moveAmount;
        agentRigidbody.AddForce(moveVector * moveSpeed, ForceMode.VelocityChange);

        //// Determine state
        if (GetCumulativeReward() <= -2.5f)
        {
            // Reward is too negative, give up
            Done();

            // Indicate failure with the ground material
            StartCoroutine(agentArea.SwapGroundMaterial(success: false));

            // Reset
            agentArea.ResetArea();
        }

        else if (GetCumulativeReward() >= 2.5f)
        {
            // Reward achieved
            Done();

            // Indicate success with the ground material
            StartCoroutine(agentArea.SwapGroundMaterial(success: true));

            // Reset
            agentArea.ResetArea();
        }
        //AddReward(0.005f);
        //Debug.Log(GetCumulativeReward());
        ////else if()
        ////{
        ////    // Success
        ////    Done();

        ////    // Indicate success with the ground material
        ////    StartCoroutine(agentArea.SwapGroundMaterial(success: true));

        ////    // Reset
        ////    agentArea.ResetArea();
        ////}
      

    }


    public override void CollectObservations()
    {
        // add raycast perception observations for stumps and walls
        float rayDistance = 10f;
        float[] rayAngles = { 90f };
        string[] detectableobjects = { "bullet", "wall" };
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableobjects, 0f, 0f));


        //add velocity observation
        Vector3 localvelocity = transform.InverseTransformDirection(agentRigidbody.velocity);
        AddVectorObs(localvelocity.x);
        AddVectorObs(localvelocity.z);
    }
    public override void AgentReset()
    {
        // Reset velocity
        agentRigidbody.velocity = Vector3.zero;
        agentRigidbody.MoveRotation(Quaternion.Euler(agentRigidbody.rotation.eulerAngles * 0f));
        // Reset number of truffles collected
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            AddReward(-0.2f);
        }
        else if (collision.gameObject.CompareTag("wall"))
        {
            AddReward(-0.05f);
        }
    }

}
