using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void beginDeath(){
        if(this.tag == "Player"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(this.gameObject.layer == LayerMask.NameToLayer("Weapons")){
            if(this.gameObject.tag == "explosion"){
                GameController.instance.player.GetComponent<PlayerPlaceBomb>().canPlace = true;
            }
            Destroy(this.gameObject);
        }
        else if(this.gameObject.layer == LayerMask.NameToLayer("universalProjectile")){
            if(this.gameObject.tag == "boomerangProjectile"){
                GameController.instance.player.GetComponent<PlayerThrowBoomerang>().canThrow = true;
            }
            // Add explosion here
            Destroy(this.gameObject);
        }
        else if(this.gameObject.layer == LayerMask.NameToLayer("Projectile")){            

            if(this.gameObject.tag == "swordProjectile"){
                GameController.instance.player.GetComponent<PlayerAttack>().swordProjectileFinish(this.gameObject.transform.position);
            }
            if(this.gameObject.tag == "arrow"){
                GameController.instance.player.GetComponent<PlayerShootArrow>().canShoot = true;
            }
            if(this.gameObject.tag == "boomerangProjectile"){
                if(this.gameObject.GetComponent<boomerangControl>().owner == GameController.instance.player){
                    GameController.instance.player.GetComponent<PlayerThrowBoomerang>().canThrow = true;
                }
                else{
                    // this.gameObject.GetComponent<boomerangControl>().owner.GetComponent<BoomerangMovement>().canMove = true;
                }
            }
            // Add explosion here
            Destroy(this.gameObject);
        }
        else if(this.gameObject.layer == LayerMask.NameToLayer("portalProjectile")){
            GameController.instance.player.GetComponent<PlayerShootPortal>().canFire = true;
            Destroy(this.gameObject);
        }
        else if(this.gameObject.layer == LayerMask.NameToLayer("Aesthetic")){
            if(this.gameObject.tag == "swordProjectileFinish"){
                GameController.instance.player.GetComponent<PlayerAttack>().willFire = true;
            }

            if(this.gameObject.tag == "liveBomb"){
                GameController.instance.player.GetComponent<PlayerPlaceBomb>().bombExplosion(this.gameObject.transform.position);
                AudioSource.PlayClipAtPoint(GameController.instance.bombBlow, Camera.main.transform.position);
            }
            Destroy(this.gameObject);
        }
        else{
            if(this.tag == "skeleton"){
                if(GetComponent<lootPossession>().hasLoot){
                    GameObject newKey = Instantiate(GetComponent<lootPossession>().loot, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity) as GameObject;
                    GameController.instance.activeObjects.Add(newKey);
                    AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                    if(GameController.instance.curRoom == new Vector2(1, 0)){
                        GameController.instance.firstSkelKeyLoc = new Vector2(transform.position.x, transform.position.y);
                    }
                    if(GameController.instance.curRoom == new Vector2(0, 4)){
                        GameController.instance.secondSkelKeyLoc = new Vector2(transform.position.x, transform.position.y);
                    }
                }
            }
            removeFromSpawn(this.gameObject.tag);

            if(GameController.instance.curRoom == new Vector2(-1, 0) && GameController.instance.objectsToSpawn[new Vector2(-1, 0)].Count == 0){
                GameObject newKey = Instantiate(GameController.instance.keyPrefab, new Vector3(26f, 2f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                GameController.instance.activeObjects.Add(newKey);
            }
            else if(GameController.instance.curRoom == new Vector2(0, 2) && GameController.instance.objectsToSpawn[new Vector2(0, 2)].Count == 0){
                GameObject newKey = Instantiate(GameController.instance.keyPrefab, new Vector3(40f, 29f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                GameController.instance.activeObjects.Add(newKey);
            }
            else if(GameController.instance.curRoom == new Vector2(-1, 2) && GameController.instance.objectsToSpawn[new Vector2(-1, 2)].Count == 0){
                AudioSource.PlayClipAtPoint(GameController.instance.openDoor, Camera.main.transform.position);
                GameController.instance.batDoor.GetComponent<SpriteRenderer>().sprite = GameController.instance.rightDoor;
                GameController.instance.batDoor.GetComponent<BoxCollider>().size = new Vector3(1f, 0.5f, 1f);
            }
            else if(GameController.instance.curRoom == new Vector2(0, 5) && GameController.instance.objectsToSpawn[new Vector2(0, 5)].Count == 0){
                GameObject newKey = Instantiate(GameController.instance.keyPrefab, new Vector3(40f, 62f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                GameController.instance.activeObjects.Add(newKey);
            }
            else if(GameController.instance.curRoom == new Vector2(2, 4) && GameController.instance.objectsToSpawn[new Vector2(2, 4)].Count == 0){
                GameObject newHeart = Instantiate(GameController.instance.bigHeart, new Vector3(76f, 49f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                GameController.instance.activeObjects.Add(newHeart);
                AudioSource.PlayClipAtPoint(GameController.instance.openDoor, Camera.main.transform.position);
                GameController.instance.dragonDoor.GetComponent<BoxCollider>().size = new Vector3(1f, 0.5f, 1f);
                GameController.instance.dragonDoor.GetComponent<SpriteRenderer>().sprite = GameController.instance.rightDoor;
            }
            else if(GameController.instance.curRoom == new Vector2(-1, 3) && GameController.instance.objectsToSpawn[new Vector2(-1, 3)].Count == 0){
                GameController.instance.oldManBlock.GetComponent<tileMovement>().canBeMoved = true;
            }
            else if(GameController.instance.curRoom == new Vector2(1, 3) && GameController.instance.objectsToSpawn[new Vector2(1, 3)].Count == 0){
                GameObject newBoom = Instantiate(GameController.instance.boomerangDrop, new Vector3(56f, 40f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                GameController.instance.activeObjects.Add(newBoom);
            }
            else{
                float drop = Random.Range(0f,1f);
                if(drop < 0.2){
                    float levelOne = 0.5f;
                    float levelTwo = 0.5f;
                    float heartChance = Random.Range(0f, 1f);
                    if(this.tag == "boomerang"){
                        levelOne = 0.33f;
                        levelTwo = 0.33f;
                    }
                    if(heartChance < 0.5){
                        GameObject newHeart = Instantiate(GameController.instance.heart, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity) as GameObject;
                        GameController.instance.activeObjects.Add(newHeart);
                    }
                    else if(heartChance <= levelOne + levelTwo){
                        GameObject newRupee = Instantiate(GameController.instance.rupee, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity) as GameObject;
                        GameController.instance.activeObjects.Add(newRupee);
                    }
                    else{
                        GameObject newBomb = Instantiate(GameController.instance.bombDrop, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity) as GameObject;
                        GameController.instance.activeObjects.Add(newBomb);
                    }
                }
            }

            AudioSource.PlayClipAtPoint(GameController.instance.deathAudio, Camera.main.transform.position);

            Destroy(this.gameObject);
        }
    }

    void removeFromSpawn(string name){
        for(int q=0; q<GameController.instance.objectsToSpawn[GameController.instance.curRoom].Count; q++){
            if(GameController.instance.objectsToSpawn[GameController.instance.curRoom][q].tag == name){
                GameController.instance.objectsToSpawn[GameController.instance.curRoom].RemoveAt(q);
                return;
            }
        }
    }
}
