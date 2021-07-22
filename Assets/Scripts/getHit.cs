using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getHit : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = -0.5f;
    public float airTime = 2f;
    public float waitTime = 0.0f;

    void Start(){
    }

    GameObject getOwner(GameObject in_){
        if(in_.tag == "boomerangProjectile"){
            return in_.GetComponent<boomerangControl>().owner;
        }
        return GameController.instance.player;
    }
    void FixedUpdate(){
        Collider[] collisions = Physics.OverlapBox(transform.position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size / 2, Quaternion.identity);
        bool inPlace = false;
        for(int q=0; q<collisions.Length; q++){
            if(Physics.GetIgnoreLayerCollision(this.gameObject.layer, collisions[q].gameObject.layer)){
                continue;
            }

            if(this.gameObject.layer == LayerMask.NameToLayer("portalProjectile")){
                this.GetComponent<Projectile>().foundTarget = true;
                GameObject wallChange = collisions[q].gameObject;
                if(wallChange.layer == LayerMask.NameToLayer("Walls") 
                    || wallChange == GameController.instance.player.GetComponent<PlayerShootPortal>().orangeReplace
                    || wallChange == GameController.instance.player.GetComponent<PlayerShootPortal>().blueReplace
                    || wallChange.transform.position == GameController.instance.player.GetComponent<PlayerShootPortal>().orangeLoc
                    || wallChange.transform.position == GameController.instance.player.GetComponent<PlayerShootPortal>().blueLoc){
                    continue;
                }
                GameObject portal = Instantiate(GameController.instance.portalPrefab, wallChange.transform.position, Quaternion.identity) as GameObject;
                GameController.instance.activeObjects.Add(portal);
                Vector3 colliderBox = new Vector3(1.2f, 1.0f, 1f);
                Sprite portalSprite = GameController.instance.bluePortalLeft;
                string wallTag = "rightPortal";
                if(wallChange.tag == "rightTile"){
                    portalSprite = GameController.instance.bluePortalRight;
                    wallTag = "leftPortal";
                }
                else if(wallChange.tag == "topTile"){
                    colliderBox = new Vector3(1.0f, 2f, 1f);
                    portalSprite = GameController.instance.bluePortalUp;
                    wallTag = "downPortal";
                }
                else if(wallChange.tag == "bottomTile"){
                    colliderBox = new Vector3(1.0f, 1.2f, 1f);
                    portalSprite = GameController.instance.bluePortalDown;
                    wallTag = "upPortal";
                }
                if(this.gameObject.GetComponent<portalProjectileController>().colorType == 0){
                    portalSprite = GameController.instance.orangePortalLeft;
                    if(wallChange.tag == "rightTile"){
                        portalSprite = GameController.instance.orangePortalRight;
                    }
                    else if(wallChange.tag == "topTile"){
                        portalSprite = GameController.instance.orangePortalUp;
                    }
                    else if(wallChange.tag == "bottomTile"){
                        portalSprite = GameController.instance.orangePortalDown;
                    }

                    if(GameController.instance.player.GetComponent<PlayerShootPortal>().orangeReplace != null){
                        Destroy(GameController.instance.player.GetComponent<PlayerShootPortal>().orangeReplace);
                    }
                    GameController.instance.player.GetComponent<PlayerShootPortal>().orangeReplace = portal;
                    GameController.instance.player.GetComponent<PlayerShootPortal>().orangeLoc = portal.transform.position;
                }
                else{
                    if(GameController.instance.player.GetComponent<PlayerShootPortal>().blueReplace != null){
                        Destroy(GameController.instance.player.GetComponent<PlayerShootPortal>().blueReplace);
                    }
                    GameController.instance.player.GetComponent<PlayerShootPortal>().blueReplace = portal;
                    GameController.instance.player.GetComponent<PlayerShootPortal>().blueLoc = portal.transform.position;
                }
                portal.GetComponent<SpriteRenderer>().sprite = portalSprite;
                portal.GetComponent<BoxCollider>().size = colliderBox;
                portal.tag = wallTag;
                GameController.instance.player.GetComponent<PlayerShootPortal>().curPortal = (GameController.instance.player.GetComponent<PlayerShootPortal>().curPortal + 1) % 2;
                AudioSource.PlayClipAtPoint(GameController.instance.portalMake, Camera.main.transform.position);
                return;
            }

            if(this.gameObject.layer == LayerMask.NameToLayer("portalPrefab")){
                if(collisions[q].gameObject.tag == "Player"){
                    if(!GameController.instance.canMove){
                        continue;
                    }
                    string ptag = this.gameObject.tag;
                    Vector2 directionLook = GameController.instance.direction;
                    if( (ptag == "upPortal" && directionLook != new Vector2(0, -1))
                        || (ptag == "downPortal" && directionLook != new Vector2(0, 1))
                        || (ptag == "leftPortal" && directionLook != new Vector2(1, 0))
                        || (ptag == "rightPortal" && directionLook != new Vector2(-1, 0)) ){
                            waitTime = 0.0f;
                            continue;
                    }
                    GameObject otherPortal = GameController.instance.player.GetComponent<PlayerShootPortal>().orangeReplace;
                    if(this.gameObject == otherPortal){
                        otherPortal = GameController.instance.player.GetComponent<PlayerShootPortal>().blueReplace;
                    }
                    if(otherPortal == null){
                        continue;
                    }

                    waitTime += Time.deltaTime;
                    if(waitTime > airTime){
                        waitTime = 0.0f;
                        GameController.instance.canMove = false;
                        GameController.instance.direction = Vector2.zero;
                        GameController.instance.directionCombo = Vector2.zero;
                        AudioSource.PlayClipAtPoint(GameController.instance.portalTeleport, Camera.main.transform.position);
                        StartCoroutine(movePlayer(otherPortal));
                    }
                }
                else if(collisions[q].gameObject.tag == "fireArrow"){
                    GameObject theArrow = collisions[q].gameObject;
                    Vector3 arrowDir = theArrow.GetComponent<arrowDirection>().flyDir;
                    if( (this.gameObject.tag == "downPortal" && arrowDir == Vector3.down)
                    || (this.gameObject.tag == "upPortal" && arrowDir == Vector3.up)
                    || (this.gameObject.tag == "leftPortal" && arrowDir == Vector3.left)
                    || (this.gameObject.tag == "rightPortal" && arrowDir == Vector3.right)){
                        continue;
                    }
                    GameObject otherPortal = GameController.instance.player.GetComponent<PlayerShootPortal>().orangeReplace;
                    if(this.gameObject == otherPortal){
                        otherPortal = GameController.instance.player.GetComponent<PlayerShootPortal>().blueReplace;
                    }
                    if(otherPortal == null){
                        continue;
                    }

                    
                    
                    AudioSource.PlayClipAtPoint(GameController.instance.portalTeleport, Camera.main.transform.position);
                    theArrow.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
         
                    if(otherPortal.tag == "leftPortal"){
                        theArrow.transform.position = new Vector3(otherPortal.transform.position.x - 1, otherPortal.transform.position.y, otherPortal.transform.position.z);
                        theArrow.GetComponent<arrowDirection>().flyDir = Vector3.left;
                        theArrow.GetComponent<SpriteRenderer>().sprite = theArrow.GetComponent<arrowDirection>().toLeftSprite;
                        theArrow.GetComponent<BoxCollider>().size = new Vector3(1f, 0.3f, 0f);
                    }
                    else if(otherPortal.tag == "rightPortal"){
                        theArrow.transform.position = new Vector3(otherPortal.transform.position.x + 1, otherPortal.transform.position.y, otherPortal.transform.position.z);
                        theArrow.GetComponent<arrowDirection>().flyDir = Vector3.right;
                        theArrow.GetComponent<SpriteRenderer>().sprite = theArrow.GetComponent<arrowDirection>().toRightSprite;
                        theArrow.GetComponent<BoxCollider>().size = new Vector3(1f, 0.3f, 0f);
                    }
                    else if(otherPortal.tag == "downPortal"){
                        theArrow.transform.position = new Vector3(otherPortal.transform.position.x, otherPortal.transform.position.y - 1f, otherPortal.transform.position.z);
                        theArrow.GetComponent<arrowDirection>().flyDir = Vector3.down;
                        theArrow.GetComponent<SpriteRenderer>().sprite = theArrow.GetComponent<arrowDirection>().toDownSprite;
                        theArrow.GetComponent<BoxCollider>().size = new Vector3(0.3f, 1f, 0f);
                    }
                    else if(otherPortal.tag == "upPortal"){
                        theArrow.transform.position = new Vector3(otherPortal.transform.position.x, otherPortal.transform.position.y + 1, otherPortal.transform.position.z);
                        theArrow.GetComponent<arrowDirection>().flyDir = Vector3.up;
                        theArrow.GetComponent<SpriteRenderer>().sprite = theArrow.GetComponent<arrowDirection>().toUpSprite;
                        theArrow.GetComponent<BoxCollider>().size = new Vector3(0.3f, 1f, 0f);
                    }


                }
                continue;
            }

            if(this.gameObject.layer == LayerMask.NameToLayer("Projectile") || this.gameObject.layer == LayerMask.NameToLayer("universalProjectile")){
                if(collisions[q].gameObject.layer == LayerMask.NameToLayer("portalPrefab")){
                    continue;
                }

                if(collisions[q].gameObject.layer == LayerMask.NameToLayer("Collect") && getOwner(this.gameObject) == GameController.instance.player){
                    if(this.gameObject.tag == "swordProjectile"){
                        continue;
                    }
                    GameObject object_collided_with = collisions[q].gameObject;
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
                    continue;
                }

                if(this.gameObject.layer == LayerMask.NameToLayer("universalProjectile")
                    && this.GetComponent<boomerangControl>().owner != GameController.instance.player
                    && collisions[q].gameObject.tag == "boomerang"
                    && collisions[q].gameObject != this.GetComponent<boomerangControl>().owner){
                        continue;
                }
                this.GetComponent<Projectile>().foundTarget = true;
                if(this.gameObject.tag == "boomerangProjectile" && collisions[q].gameObject == this.GetComponent<boomerangControl>().owner){
                    this.GetComponent<Projectile>().foundOwner = true;
                    if(this.GetComponent<boomerangControl>().owner != GameController.instance.player){  
                        this.GetComponent<boomerangControl>().owner.GetComponent<BoomerangMovement>().canMove = true;
                    }
                    return;
                }
            }

            Hitbox otherHitbox = collisions[q].gameObject.GetComponent<Hitbox>();
            if(otherHitbox == null || otherHitbox.enabled == false){
                continue;
            }
            if(this.gameObject.tag == "boomerangProjectile"){
                if(this.GetComponent<boomerangControl>().owner == GameController.instance.player){
                    if(collisions[q].gameObject.tag == "bat" || collisions[q].gameObject.tag == "gel")
                    {
                        otherHitbox.OnHit(Vector2.zero, damage);
                    }
                    otherHitbox.OnStun();
                    return;
                }
                else{
                    if(collisions[q].gameObject.tag != "Player"){
                        continue;
                    }
                    damage = -1;
                }
            }
            if(otherHitbox.invulnerable){
                continue;
            }
            Vector2 dir = (transform.position - collisions[q].transform.position);
            if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y)){
                if(dir.x > 0){
                    dir = Vector2.right;
                }
                else{
                    dir = Vector2.left;
                }
            }
            else{
                if(dir.y > 0){
                    dir = Vector2.up;
                }
                else{
                    dir = Vector2.down;
                }
            }
            otherHitbox.OnHit(dir * -1, damage);
        }
        if(!inPlace){
            waitTime = airTime;
        }
    }

    IEnumerator movePlayer(GameObject otherPortal){
         GameController.instance.player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
         GameController.instance.player.GetComponent<Animator>().speed = 0.0f;
        if(otherPortal.tag == "leftPortal"){
            GameController.instance.player.transform.position = new Vector3(otherPortal.transform.position.x - 1, otherPortal.transform.position.y, otherPortal.transform.position.z);
            GameController.instance.player.GetComponent<SpriteRenderer>().sprite = GameController.instance.linkleft;
        }
        else if(otherPortal.tag == "rightPortal"){
            GameController.instance.player.transform.position = new Vector3(otherPortal.transform.position.x + 1, otherPortal.transform.position.y, otherPortal.transform.position.z);
            GameController.instance.player.GetComponent<SpriteRenderer>().sprite = GameController.instance.linkRight;
        }
        else if(otherPortal.tag == "downPortal"){
            GameController.instance.player.transform.position = new Vector3(otherPortal.transform.position.x, otherPortal.transform.position.y - 1, otherPortal.transform.position.z);
            GameController.instance.player.GetComponent<SpriteRenderer>().sprite = GameController.instance.linkDown;
        }
        else if(otherPortal.tag == "upPortal"){
            GameController.instance.player.transform.position = new Vector3(otherPortal.transform.position.x, otherPortal.transform.position.y + 1, otherPortal.transform.position.z);
            GameController.instance.player.GetComponent<SpriteRenderer>().sprite = GameController.instance.linkUp;
        }

        
        

        yield return new WaitForSeconds(0.25f);

        GameController.instance.canMove = true;
    }
}
