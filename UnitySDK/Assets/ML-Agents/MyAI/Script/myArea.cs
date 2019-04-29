using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myArea : Area
{
    [Header("Area Objects")]
    public GameObject myObject;
    public GameObject myBullet;
    public GameObject ground;
    public Material successMaterial;
    public Material failureMaterial;
    public float respawnTime = 0.1f;
     //public TextMeshPro scoreText;

    [HideInInspector]
    public int numTruffles;
    [HideInInspector]
    public int numStumps;
    [HideInInspector]
    public float spawnRange = 2f;

    private List<GameObject> spawnedBullet;
    //[Header("Area Object")]
    //public GammeObject agentCube;

    //public override void ResetArea()
    //{
    //    ResetAgent();
    //}

    //private void ResetAgent()
    //{
    //    //RandomPlaceObject(agentCube, spawnrange, 10);
    //}

    //private void reseetArea()
    //{

    //}
    LayerMask notGroundLayerMask;
    private Renderer groundRenderer;
    private Material groundMaterial;
    private void Start()
    {
        groundRenderer = ground.GetComponent<Renderer>();
        groundMaterial = groundRenderer.material;
        notGroundLayerMask = ~LayerMask.GetMask("ground");
        StartCoroutine(bulletWave());
    }

    public override void ResetArea()
    {
        Debug.Log("reset area");
        ResetAgent();
    }

    public IEnumerator SwapGroundMaterial(bool success)
    {
        if (success)
        {
            groundRenderer.material = successMaterial;
        }
        else
        {
            groundRenderer.material = failureMaterial;
        }
        yield return new WaitForSeconds(0.5f);
        groundRenderer.material = groundMaterial;
    }

    private void RandomlyPlaceObject(GameObject objectToPlace, float range, float maxAttempts)
    {
        Debug.Log(objectToPlace);
        // Temporarily disable collision-
        objectToPlace.GetComponent<Collider>().enabled = true;

        // Calculate test radius 10% larger than the collider extents
        float testRadius = GetColliderRadius(objectToPlace) * 1.1f;

        // Set a random rotation
        objectToPlace.transform.rotation = Quaternion.Euler(new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f));

        // Make several attempts at randomly placing the object
        int attempt = 1;
        while (attempt <= maxAttempts)
        {
            Vector3 randomLocalPosition = new Vector3(UnityEngine.Random.Range(-range, range), 0, UnityEngine.Random.Range(-range, range));
            randomLocalPosition.Scale(transform.localScale);

            if (!Physics.CheckSphere(transform.position + randomLocalPosition, testRadius, notGroundLayerMask))
            {
                objectToPlace.transform.localPosition = randomLocalPosition;
                Debug.Log("PLACED");
                break;
            }
            else if (attempt == maxAttempts)
            {
                Debug.LogError(string.Format("{0} couldn't be placed randomly after {1} attempts.", objectToPlace.name, maxAttempts));
                break;
            }

            attempt++;
        }
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

    //public void UpdateScore(float score)
    //{
    //    scoreText.text = score.ToString("0.00");
    //}
    private void ResetAgent()
    {
        // Reset location and rotation
        //transform.rotation.y = 0;
        RandomlyPlaceObject(myObject, spawnRange, 11);
    }

    private void spawnBullet()
    {
        GameObject bulletObect = Instantiate(myBullet) as GameObject;
        RandomlyPlaceObject(bulletObect, spawnRange, 50);
        bulletObect.AddComponent<bulletBehavior>();
        bulletObect.transform.position = new Vector3(Random.Range(this.transform.position.x + 4.5f, this.transform.position.x - 4.5f), -2.5f, this.transform.position.z + 4.5f);
        //bulletObect.transform.position = new Vector3(this.transform.position.x -4.5f ,-2.5f, Random.Range(this.transform.position.z + 4.5f, this.transform.position.z - 4.5f));
    }

    IEnumerator bulletWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnBullet();
        }
    }
    //private void ResetBullet()
    //private void ResetBullet()
    //{
    //    if (spawnedBullet != null)
    //    {
    //        // Destroy any truffles remaining from the previous run
    //        foreach (GameObject spawnedMushroom in spawnedBullet.ToArray())
    //        {
    //            Destroy(spawnedMushroom);
    //        }
    //    }

    //    spawnedBullet = new List<GameObject>();

    //    for (int i = 0; i < numTruffles; i++)
    //    {
    //        // Create a new truffle instance and place it randomly
    //        GameObject truffleInstance = Instantiate(myObject, transform);
    //        RandomlyPlaceObject(truffleInstance, spawnRange, 50);
    //        spawnedBullet.Add(truffleInstance);
    //    }
    //}

}
