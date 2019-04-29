using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public GameObject spawnPointWall;
    float respawnTime = 1.0f;
    void Start()
    {
        StartCoroutine(bulletWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawn()
    {
        GameObject temp = Instantiate(bullet) as GameObject;
        float random = Random.Range(-3f, 3f);
        temp.transform.position = new Vector3(spawnPointWall.transform.position.x + 1, -2, spawnPointWall.transform.position.z + random);
        temp.AddComponent<bulletBehavior>();
    }

    IEnumerator bulletWave()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawn();
        }
    }
}
