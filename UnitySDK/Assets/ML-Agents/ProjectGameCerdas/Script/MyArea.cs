using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyArea : MonoBehaviour
{
    [Header("Area Objects")]
    public GameObject[] spawner;
    public GameObject box;

    private MyAcademy academy;
    private bool boxSpawn = true;
    private double timeLeft = 0;
    // Start is called before the first frame update
    void Awake()
    {
        academy = FindObjectOfType<MyAcademy>(); //cache the academy
    }

    // Update is called once per frame
    void Update()
    {
        GameObject check = GameObject.FindWithTag("crate");
        timeLeft -= Time.deltaTime;
        if(check == null && boxSpawn == true)
        {
            Debug.Log(boxSpawn);
            timeLeft = 3;
            boxSpawn = false;
        }

        if (timeLeft <= 0 && boxSpawn == false)
        {
            spawn();
            boxSpawn = true;
        }
    }
    //public override void ResetArea()
    //{
    //    occupiedPositions = new List<Tuple<Vector3, float>>();
    //    ResetAgent();
    //    ResetTruffles();
    //    ResetStumps();
    //}
    public void spawn()
    {
        int rand = Random.Range(0, 4);
        GameObject cube = Instantiate(box, spawner[rand].transform.position, 
                                        spawner[rand].transform.rotation);
        boxSpawn = true;
        
    }
}
