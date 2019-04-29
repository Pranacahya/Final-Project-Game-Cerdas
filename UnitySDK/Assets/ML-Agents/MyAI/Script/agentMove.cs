using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agentMove : MonoBehaviour
{

    public void MoveAgent(float[] act)
    {

        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        int action = Mathf.FloorToInt(act[0]);

        // Goalies and Strikers have slightly different action spaces.
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
            case 5:
                dirToGo = transform.right * -0.75f;
                break;
            case 6:
                dirToGo = transform.right * 0.75f;
                break;
        }
        //transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        //agentRB.AddForce(dirToGo * academy.agentRunSpeed,
        //                 ForceMode.VelocityChange);

    }
}
