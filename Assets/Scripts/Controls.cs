using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    // Start is called before the first frame update
    Inventory playerInventory;
    Health playerHealth;
    void Start()
    {
        playerInventory = GetComponent<Inventory>();   
        playerHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.instance.canMove){
            return;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            GameController.instance.god_mode = !GameController.instance.god_mode;
            if(GameController.instance.god_mode == true){
                Debug.Log("God Mode Turned On!");
                playerInventory.prev_rupee = playerInventory.rupee_count;
                playerInventory.prev_bomb = playerInventory.bomb_count;
                playerInventory.prev_key = playerInventory.key_count;
                playerInventory.hasBowPrev = playerInventory.hasBow;
                playerInventory.hasBoomerangPrev = playerInventory.hasBoomerang;
                GameController.instance.player.GetComponent<weaponSwitch>().curWep = -1;
                GameController.instance.player.GetComponent<weaponSwitch>().leftUI.sprite = GameController.instance.black;

                playerInventory.rupee_count = 99;
                playerInventory.bomb_count = 99;
                playerInventory.key_count = 99;
                playerInventory.hasBow = true;
                playerInventory.hasBoomerang = true;
                playerHealth.currHealth = playerHealth.maxHealth;
                GameController.instance.player.layer = LayerMask.NameToLayer("God");
                GameController.instance.player.GetComponent<Hitbox>().enabled = false;
            }
            else{
                Debug.Log("God Mode Turned Off!");
                playerInventory.hasBow = playerInventory.hasBowPrev;
                playerInventory.hasBoomerang = playerInventory.hasBoomerangPrev;
                GameController.instance.player.GetComponent<weaponSwitch>().curWep = -1;
                GameController.instance.player.GetComponent<weaponSwitch>().leftUI.sprite = GameController.instance.black;
                
                playerInventory.rupee_count = playerInventory.prev_rupee;
                playerInventory.bomb_count = playerInventory.prev_bomb;
                playerInventory.key_count = playerInventory.prev_key;
                GameController.instance.player.layer = LayerMask.NameToLayer("Player");
                GameController.instance.player.GetComponent<Hitbox>().enabled = true;
            }
        }
    }
}
