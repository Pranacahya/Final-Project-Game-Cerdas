using MLAgents;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyArea : Area
{
    [Header("Area Objects")]
    public GameObject enemy;
    public TextMeshPro scoreText;
    public GameObject[] spawner;
    public GameObject[] enemySpawner;
    public GameObject box;
    public GameObject player;
    public float spawnRange;
    private MyAcademy academy;
    private bool boxSpawn = true;
    private double timeLeft = 0;
    private List<Tuple<Vector3, float>> occupiedPositions;


    // Start is called before the first frame update
    void Awake()
    {
        spawn();
        academy = FindObjectOfType<MyAcademy>(); //cache the academy
    }

    public override void ResetArea()
    {
        ResetAgent();
    }
    public void UpdateScore(float score)
    {
        scoreText.text = score.ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject check = GameObject.FindWithTag("crate");
        timeLeft -= Time.deltaTime;
        if(check == null && boxSpawn == true)
        {
            //timeLeft = 3;
            boxSpawn = false;
        }

        if (timeLeft <= 0 && boxSpawn == false)
        {
            //spawn();
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

        int rand = UnityEngine.Random.Range(0,spawner.Length-1);
        GameObject cube = Instantiate(box, spawner[rand].transform.position, 
                                        spawner[rand].transform.rotation);
     
    }

    public void ResetAgent()
    {
        RandomlyPlaceObject(player, spawnRange, 10);
    }
    private void RandomlyPlaceObject(GameObject objectToPlace, float range, float maxAttempts)
    {
        // Temporarily disable collision
        objectToPlace.GetComponent<Collider>().enabled = false;

        // Calculate test radius 10% larger than the collider extents
        float testRadius = GetColliderRadius(objectToPlace) * 1.1f;

        // Set a random rotation
        objectToPlace.transform.rotation = Quaternion.Euler(new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f));
        Vector3 randomLocalPosition = new Vector3(UnityEngine.Random.Range(-range, range), 0, UnityEngine.Random.Range(-range, range));
        randomLocalPosition.Scale(transform.localScale);

        // Make several attempts at randomly placing the object

        // Enable collision
        objectToPlace.GetComponent<Collider>().enabled = true;
    }

    private static float GetColliderRadius(GameObject obj)
    {
        Collider col = obj.GetComponent<Collider>();

        Vector3 boundsSize = Vector3.zero;
        if (col.GetType() == typeof(MeshCollider))
        {
            boundsSize = ((MeshCollider)col).sharedMesh.bounds.size;
        }
        else if (col.GetType() == typeof(BoxCollider))
        {
            boundsSize = col.bounds.size;
        }

        boundsSize.Scale(obj.transform.localScale);
        return Mathf.Max(boundsSize.x, boundsSize.z) / 2f;
    }

    private bool CheckIfPositionIsOpen(Vector3 testPosition, float testRadius)
    {
        foreach (Tuple<Vector3, float> occupied in occupiedPositions)
        {
            Vector3 occupiedPosition = occupied.Item1;
            float occupiedRadius = occupied.Item2;
            if (Vector3.Distance(testPosition, occupiedPosition) - occupiedRadius <= testRadius)
            {
                return false;
            }
        }

        return true;
    }

    public void spawnEnemy()
    {
        int rand2 = UnityEngine.Random.Range(0, enemySpawner.Length - 1);
        enemy.transform.position = enemySpawner[rand2].transform.position;
    }

    public void spawnMe()
    {
        int rand2 = UnityEngine.Random.Range(0, spawner.Length - 1);
        player.transform.position = spawner[rand2].transform.position;
    }
}
