using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
public class PlayerAgent : Agent
{
    [Header("Enemy Agent Settings")]
    public float moveSpeed = 1f;
    public float rotateSpeed = 2f;
    public GameObject bulletObj;

    private double timeLeft = 3f;
    private bool handToggle;
    private bool shootToggle;
    private WeaponScript playerWeapon;
    private MyAcademy agentAcademy;
    private MyArea agentArea;
    PlayerScript me;
    float lastShot;
    float fireRate;
    GameObject firePoint;
    Rigidbody agentRigidbody;
    RayPerception rayPerception;
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        if (shootToggle)
        {
            handToggle = false;
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Debug.Log("hand");
                timeLeft = 0.5;
                handToggle = true;
            }
        }
    }
    public override void InitializeAgent()
    {
        handToggle = true;
        shootToggle = false;
        base.InitializeAgent();
        agentAcademy = FindObjectOfType<MyAcademy>();
        agentArea = transform.parent.GetComponent<MyArea>();
        agentRigidbody = GetComponent<Rigidbody>();
        agentRigidbody = GetComponent<Rigidbody>();
        rayPerception = GetComponent<RayPerception>();
        agentRigidbody = GetComponent<Rigidbody>();
        firePoint = transform.GetChild(2).gameObject;
        me = new PlayerScript("dadang");
        ResetPlayer();
    }

    public override void CollectObservations()
    {
        // Add raycast perception observations for stumps and walls
        float rayDistance = 20f;
        float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 110f, 70f };
        string[] detectableObjects = { "Player", "wall", "crate", "bullet" };
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 1.5f, 0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "crate")
        {
            me.myWeapon.SetAmo(30);
            Destroy(collision.gameObject);
        }

        if (collision.transform.tag == "wall" || collision.transform.tag == "player")
        {
            AddReward(-0.01f);
        }

        if(collision.transform.name == "MachinegunCrate")
        {
            me.myWeapon.SetWeapon("Machinegun");
            Destroy(collision.gameObject);
        }
    }
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Determine the rotation action
        float rotateAmount = 0;
        if (vectorAction[1] == 1)
        {
            //transform.Rotate(0f, 0f, 0f);
            rotateAmount = -rotateSpeed;
        }
        else if (vectorAction[1] == 2)
        {
            rotateAmount = rotateSpeed;
            //transform.Rotate(0f, 180f, 0f);
            //firePoint.transform.Rotate(0f, 180f, 0f);
        }
        //transform.Rotate(0f,0f,0f);
        //Apply the rotation
        Vector3 rotateVector = transform.up * rotateAmount;
        agentRigidbody.MoveRotation(Quaternion.Euler(agentRigidbody.rotation.eulerAngles + rotateVector * rotateSpeed));

        // Determine move action
        float moveAmount = 0;
        if (vectorAction[0] == 1)
        {
            moveAmount = moveSpeed;
        }
        else if (vectorAction[0] == 2)
        {
            moveAmount = moveSpeed * -1f; // move at half-speed going backwards
        }

        if (vectorAction[2] == 1 && me.myWeapon.GetWeapon() == "Handgun")
        {
            shootToggle = true;

            if (me.myWeapon.GetAmo() != 0 && handToggle)
            {
                Shoot();
            }
        }
        else
        {
            shootToggle = false;
        }
        if (vectorAction[2] == 1 && me.myWeapon.GetWeapon() == "Machinegun")
        {
            Shoot();
        }
       
        handToggle = true;
        // Apply the movement
        Vector3 moveVector = transform.forward * moveAmount;
        agentRigidbody.AddForce(moveVector * moveSpeed, ForceMode.VelocityChange);
        // Determine state
    }

    public void Shoot()
    {
        Debug.Log(me.myWeapon.GetAmo());
        Debug.Log(me.myWeapon.GetWeapon());
        if (me.myWeapon.GetWeapon() == "Handgun" && handToggle && me.myWeapon.GetAmo() > 0)
        {
            handToggle = false;
            me.myWeapon.Shoot();
            GameObject bulletObject = Instantiate(bulletObj, firePoint.transform.position, firePoint.transform.rotation);
            bulletObject.AddComponent<bulletBehavior>();
            bulletObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10f);
        }

        else if (me.myWeapon.GetWeapon() == "Machinegun" && me.myWeapon.GetAmo() >0)
        {
            me.myWeapon.Shoot();
            GameObject bulletObject = Instantiate(bulletObj, firePoint.transform.position, firePoint.transform.rotation);
            bulletObject.AddComponent<bulletBehavior>();
            bulletObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10f);
            //bulletObect.GetComponent<bulletBehavior>().setPower(me.getPower());
        }
        if(me.myWeapon.GetWeapon() == "Machinegun" && me.myWeapon.GetAmo() == 0)
        {
            Debug.Log("test");
            me.myWeapon.SetWeapon("Handgun");
        }
    }
    
    public override void AgentReset()
    {
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        agentRigidbody.velocity = Vector3.zero;
        me = new PlayerScript("dadang");
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }
}
