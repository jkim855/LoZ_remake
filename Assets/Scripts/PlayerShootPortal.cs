using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootPortal : MonoBehaviour
{
    public bool canFire = true;
    weaponSwitch weapon;
    Animator animator;

    float portalOffset = 0.8f;

    public int curPortal = 1;

    public GameObject portalGunPrefab;
    public GameObject portalProjectilePrefab;
    float projectileSpeed = 10.0f;

    void Start(){
        weapon = GetComponent<weaponSwitch>();
        animator = GetComponent<Animator>();
    }

    public GameObject orangeReplace;
    public GameObject blueReplace;

    public Vector3 orangeLoc;
    public Vector3 blueLoc;

    public IEnumerator Fire(){
        canFire = false;
        GameController.instance.canMove = false;
        
        AudioSource.PlayClipAtPoint(GameController.instance.portalShoot, Camera.main.transform.position);

        GameController.instance.player.GetComponent<Rigidbody>().velocity = new Vector2(0.0f, 0.0f);
        animator.SetFloat("horizontal_input", 0.0f);
        animator.SetFloat("vertical_input", 0.0f);

        int directionFace = GameController.instance.directionFace;
        Sprite portalGunSprite = GameController.instance.leftPortalGun;
        Sprite portalProjectile = GameController.instance.bluePortalProjectile;
        if(curPortal == 0){
            portalProjectile = GameController.instance.orangePortalProjectile;
        }
        Vector3 portalGunLoc = new Vector3(transform.position.x - portalOffset, transform.position.y, transform.position.z);
        float xBoxOffset = 0.0f;
        float yBoxOffset = 0.0f;

        if(directionFace == 0){
        //left
            animator.SetInteger("leftSwordAttack", 1);
            xBoxOffset = -1 * (1-portalOffset);
        }
        else if(directionFace == 1){
        //right
            animator.SetInteger("rightSwordAttack", 1);
            portalGunSprite = GameController.instance.rightPortalGun;
            portalGunLoc = new Vector3(transform.position.x + portalOffset, transform.position.y, transform.position.z);
            xBoxOffset = 1-portalOffset;
        }
        else if(directionFace == 2){
        // up
            animator.SetInteger("upSwordAttack", 1);
            portalGunSprite = GameController.instance.upPortalGun;
            portalGunLoc = new Vector3(transform.position.x, transform.position.y + portalOffset, transform.position.z);
            yBoxOffset = 1-portalOffset;
        }
        else if(directionFace == 3){
        // down
            animator.SetInteger("downSwordAttack", 1);
            portalGunSprite = GameController.instance.downPortalGun;
            portalGunLoc = new Vector3(transform.position.x, transform.position.y - portalOffset, transform.position.z);
            yBoxOffset = -1 * (1-portalOffset);
        }
        // Time of animation
        GameObject newPortalGun = Instantiate(portalGunPrefab, portalGunLoc, Quaternion.identity) as GameObject;
        newPortalGun.GetComponent<SpriteRenderer>().sprite = portalGunSprite;
		yield return new WaitForSeconds(0.1f);
        StartCoroutine(firePortal(portalGunLoc, directionFace, portalProjectile));
        yield return new WaitForSeconds(0.1f);
        Destroy(newPortalGun);

        animator.SetInteger("leftSwordAttack", 0);
        animator.SetInteger("rightSwordAttack", 0);
        animator.SetInteger("upSwordAttack", 0);
        animator.SetInteger("downSwordAttack", 0);
        GameController.instance.canMove = true;
    }

    IEnumerator firePortal(Vector3 portalGunLoc, int directionFace, Sprite portalProjectile){
        Vector3 directionVector = new Vector3(-1.0f, 0.0f, 0.0f);

        if(directionFace == 1){
            directionVector = new Vector3(1.0f, 0.0f, 0.0f);
        }
        if(directionFace == 2){
            directionVector = new Vector3(0.0f, 1.0f, 0.0f);
        }
        if(directionFace == 3){
            directionVector = new Vector3(0.0f, -1.0f, 0.0f);
        }

        GameObject newPortalProjectile = Instantiate(portalProjectilePrefab, portalGunLoc, Quaternion.identity) as GameObject;
        GameController.instance.activeObjects.Add(newPortalProjectile);
        newPortalProjectile.GetComponent<SpriteRenderer>().sprite = portalProjectile;
        newPortalProjectile.GetComponent<portalProjectileController>().colorType = curPortal;
        StartCoroutine(weapon.blink(newPortalProjectile));
        Vector3 combo = GameController.instance.directionCombo;
        if(combo.x != 0 && combo.y != 0){
            directionVector = combo.normalized;
        }
        while(true){
            if(newPortalProjectile == null){
                break;
            }
            newPortalProjectile.GetComponent<Rigidbody>().velocity = directionVector * projectileSpeed;
            yield return new WaitForSeconds(.1f);
        }
    }
}
