using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public WeaponScript myWeapon;
    string nama;
    int HP;

    public void Update()
    {
        //if(HP<=0)
        //{       
        //    Debug.Log(nama + "is dead");
        //    Destroy(this.gameObject);
        //}
    }
    public PlayerScript(string nama)
    {
        myWeapon = new WeaponScript();
        this.nama = nama;
        this.HP = 200;
    }

    public void SetHP(int hp)
    {
        this.HP = hp;
    }

    public int GetHP()
    {
        return this.HP;
    }

    public void Attack(PlayerScript enemy)
    {
        //enemy.setHP(power);
    }

    public void SetNama(string nama)
    {
        this.nama = nama;
    }

    public string GetNama()
    {
        return this.nama;
    }
}

