using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int rupee_count = 0;
    public int bomb_count = 0;
    public int prev_rupee = 0;
    public int prev_bomb = 0;
    public int prev_key = 0;
    public int key_count = 0;



    public bool hasBow = false;
    public bool hasBowPrev = false;

    public bool hasBoomerang = false;
    public bool hasBoomerangPrev = false;

    public bool hasPortalGun = false;
    public bool hasPortalGunPrev = false;



    public void AddRupees(int num_rupees){
        if(GameController.instance.god_mode){
            return;
        }
        rupee_count += num_rupees;
    }

    public void AddKey(int num_keys){
        if(GameController.instance.god_mode){
            return;
        }
        key_count += num_keys;
    }

    public void AddBombs(int num_bombs){
        if(GameController.instance.god_mode){
            return;
        }
        key_count += num_bombs;
    }

    public int GetRupees(){
        return rupee_count;
    }

    public int GetKeys(){
        return key_count;
    }

    public int GetBombs(){
        return bomb_count;
    }
}
