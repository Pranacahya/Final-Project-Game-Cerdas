using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject bulletObj;
    GameObject firePoint;
    public bulletBehavior myBullet;
    int tempAmo;
    int amo;
    int power;
    string myWeapon;
    string[] weapons = new string[]{ "Handgun","Shotgun","Machinegun" };

    public WeaponScript()
    {
        this.myWeapon = weapons[0];
        this.power = 10;
        this.amo = 999999;
    }

    // Update is called once per frame
    public void Shoot()
    {
        //bulletObj = (GameObject)Resources.Load("prefabs/Bullet", typeof(GameObject));
        //Debug.Log(bulletObj.transform.tag);
        //GameObject bulletObject = Instantiate(bulletObj, firePoint.transform.position, firePoint.transform.rotation);
        //bulletObject.AddComponent<bulletBehavior>();
        //bulletObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 10f);
        this.amo -= 1;
    }

    public string GetWeapon()
    {
        return this.myWeapon;
    }

    public void SetAmo(int amo)
    {
        this.amo = amo;
    }

    public int GetAmo()
    {
        return this.amo;
    }

    public void SetPower(int power)
    {
        this.power = power;
    }

    public int GetPower()
    {
        return this.power;
    }

    public void SetWeapon(string weapon)
    {
        if(weapon == "Handgun")
        {
            this.amo = tempAmo;
            this.myWeapon = weapon;
        }
        else if(weapon == "Machinegun")
        {
            tempAmo = this.amo;
            this.myWeapon = weapon;
            this.amo = 100;
        }
    }
}
