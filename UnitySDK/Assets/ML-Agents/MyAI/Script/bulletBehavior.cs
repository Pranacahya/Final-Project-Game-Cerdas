using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public PlayerAgent playerAgent;
    int power;
    bool hitPlayer = false;
    //public bulletBehavior(int power)
    //{
    //    this.power = power;
    //}

    void Start()
    {

    }

    //public void setPower(int power)
    //{
    //    this.power = power;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            playerAgent.BulletHit("wall");
            playerAgent.AddReward(-0.001f);
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.transform.tag == "Player")
        {
            playerAgent.BulletHit("player");
            PlayerScript PS = collision.gameObject.GetComponent<PlayerScript>();
            Debug.Log("my name " + collision.gameObject.transform.name);
            //Debug.Log(collision.gameObject.GetComponent<PlayerScript>().getAmo());
            Destroy(this.gameObject);
        }
        
    }
    //public float HitReward()
    //{
    //    if(hitPlayer == true && obj == "Player")
    //    {
    //        hitPlayer = false;
    //        return 10f;
    //    }
    //    return 0;
    //}
}
