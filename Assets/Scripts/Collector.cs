using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{

    void Start(){
        if(GameController.instance.player.GetComponent<Inventory>() == null){
            Debug.LogWarning("WARNING: Gameobject with a collector has no inventory to store things in!");
        }
        if(GameController.instance.player.GetComponent<Health>() == null){
            Debug.LogWarning("WARNING: Gameobject with a collector has no health to add to!");
        }
    }
    void OnTriggerEnter(Collider collider){
        GameObject object_collided_with = collider.gameObject;

        if(object_collided_with.tag == "rupee"){
            if(GameController.instance.player.GetComponent<Inventory>() != null){
                GameController.instance.player.GetComponent<Inventory>().AddRupees(1);
            }
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(GameController.instance.rupee_collection_sound_clip, Camera.main.transform.position);
            if(GameController.instance.curRoom == new Vector2(21, 22)){
                GameController.instance.gotEndLoot = true;
            }
        }

        if(object_collided_with.tag == "heart"){
            if(GameController.instance.player.GetComponent<Health>() != null){
                GameController.instance.player.GetComponent<Health>().AddHealth(1);
            }
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(GameController.instance.health_collection_sound_clip, Camera.main.transform.position);
        }

        if(object_collided_with.tag == "key"){
            if(GameController.instance.player.GetComponent<Inventory>() != null){
                GameController.instance.player.GetComponent<Inventory>().AddKey(1);
            }
            Destroy(object_collided_with);
            if(GameController.instance.curRoom == new Vector2(-1, 0)){
                GameController.instance.batKey = true;
            }
            if(GameController.instance.curRoom == new Vector2(1, 0)){
                GameController.instance.firstSkelKey = true;
            }
            if(GameController.instance.curRoom == new Vector2(2, 3)){
                GameController.instance.handKey = true;
            }
            if(GameController.instance.curRoom == new Vector2(0, 5)){
                GameController.instance.waterKey = true;
            }
            if(GameController.instance.curRoom == new Vector2(0, 2)){
                GameController.instance.midKey = true;
            }
            if(GameController.instance.curRoom == new Vector2(0, 4)){
                GameController.instance.secondSkelKey = true;
            }

            AudioSource.PlayClipAtPoint(GameController.instance.key_collection_sound_clip, Camera.main.transform.position);
        } 

        if(object_collided_with.tag == "bigHeart"){
            if(GameController.instance.player.GetComponent<Health>() != null){
                GameController.instance.player.GetComponent<Health>().maxHealth += 1;
            }
            AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
            GameController.instance.hasBigHeart = true;
            Destroy(object_collided_with);
        }  

        if(object_collided_with.tag == "bow"){
            if(GameController.instance.player.GetComponent<Inventory>() != null){
                GameController.instance.player.GetComponent<Inventory>().hasBow = true;
                GameController.instance.player.GetComponent<Inventory>().hasBowPrev = true;
            }
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(GameController.instance.findAudio, Camera.main.transform.position);
        }

        if(object_collided_with.tag == "bombDrop"){
            if(GameController.instance.player.GetComponent<Inventory>() != null){
                GameController.instance.player.GetComponent<Inventory>().AddBombs(1);
            }
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(GameController.instance.bomb_collection_sound_clip, Camera.main.transform.position);
        }

        if(object_collided_with.tag == "boomerangDrop"){
            if(GameController.instance.player.GetComponent<Inventory>() != null){
                GameController.instance.player.GetComponent<Inventory>().hasBoomerangPrev = true;
                GameController.instance.player.GetComponent<Inventory>().hasBoomerang = true;
            }
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(GameController.instance.bomb_collection_sound_clip, Camera.main.transform.position);
        }

        if(object_collided_with.tag == "portalGun"){
            if(GameController.instance.player.GetComponent<Inventory>() != null){
                GameController.instance.player.GetComponent<Inventory>().hasPortalGun = true;
                GameController.instance.player.GetComponent<Inventory>().hasPortalGunPrev = true;
            }
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(GameController.instance.findAudio, Camera.main.transform.position);
        }
    }
}
