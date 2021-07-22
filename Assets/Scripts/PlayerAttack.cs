using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public bool canAttack = true;
    public bool willFire = true;

    public GameObject swordPrefab;
    public GameObject swordProjectilePrefab;
    public GameObject swordProjectileFinishPrefab;
    Animator animator;
    BoxCollider hitbox;
    weaponSwitch weapon;
    float swordOffset = 0.8f;

    float projectileSpeed = 10.0f;
    void Start(){
        animator = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider>();
        weapon = GetComponent<weaponSwitch>();
    }

	public IEnumerator Attack() {
		canAttack = false;
        GameController.instance.canMove = false;
        GameController.instance.player.GetComponent<Rigidbody>().velocity = new Vector2(0.0f, 0.0f);
        animator.SetFloat("horizontal_input", 0.0f);
        animator.SetFloat("vertical_input", 0.0f);

        int directionFace = GameController.instance.directionFace;
        Sprite swordSprite = GameController.instance.leftSword;
        Sprite swordProjectileSprite = GameController.instance.leftSwordProjectile;
        Vector3 swordLocation = new Vector3(transform.position.x - swordOffset, transform.position.y, transform.position.z);
        float xBoxOffset = 0.0f;
        float yBoxOffset = 0.0f;

        if(directionFace == 0){
        //left
            animator.SetInteger("leftSwordAttack", 1);
            xBoxOffset = -1 * (1-swordOffset);
        }
        else if(directionFace == 1){
        //right
            animator.SetInteger("rightSwordAttack", 1);
            swordSprite = GameController.instance.rightSword;
            swordProjectileSprite = GameController.instance.rightSwordProjectile;
            swordLocation = new Vector3(transform.position.x + swordOffset, transform.position.y, transform.position.z);
            xBoxOffset = 1-swordOffset;
        }
        else if(directionFace == 2){
        // up
            animator.SetInteger("upSwordAttack", 1);
            swordSprite = GameController.instance.upSword;
            swordProjectileSprite = GameController.instance.upSwordProjectile;
            swordLocation = new Vector3(transform.position.x, transform.position.y + swordOffset, transform.position.z);
            yBoxOffset = 1-swordOffset;
        }
        else if(directionFace == 3){
        // down
            animator.SetInteger("downSwordAttack", 1);
            swordSprite = GameController.instance.downSword;
            swordProjectileSprite = GameController.instance.downSwordProjectile;
            swordLocation = new Vector3(transform.position.x, transform.position.y - swordOffset, transform.position.z);
            yBoxOffset = -1 * (1-swordOffset);
        }
        // Time of animation
        GameObject newSword = Instantiate(swordPrefab, swordLocation, Quaternion.identity) as GameObject;
        newSword.GetComponent<SpriteRenderer>().sprite = swordSprite;
        newSword.GetComponent<BoxCollider>().center = new Vector3(xBoxOffset, yBoxOffset, newSword.GetComponent<BoxCollider>().center.z);
        AudioSource.PlayClipAtPoint(GameController.instance.swordSwing, Camera.main.transform.position);
		yield return new WaitForSeconds(0.1f);
        if(willFire){
            Health playerHealth = GameController.instance.player.GetComponent<Health>();
            if(playerHealth.currHealth == playerHealth.maxHealth){
                StartCoroutine(fireSword(swordLocation, directionFace, swordProjectileSprite));
            }
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(newSword);

        // reset
		canAttack = true;

        animator.SetInteger("leftSwordAttack", 0);
        animator.SetInteger("rightSwordAttack", 0);
        animator.SetInteger("upSwordAttack", 0);
        animator.SetInteger("downSwordAttack", 0);
        GameController.instance.canMove = true;
	}

    IEnumerator fireSword(Vector3 location, int directionFace, Sprite swordProjectileSprite){
        willFire = false;
        AudioSource.PlayClipAtPoint(GameController.instance.swordFire, Camera.main.transform.position);
        Vector3 directionVector = new Vector3(-1.0f, 0.0f, 0.0f);
        Vector3 boxSize = new Vector3(1.0f, 0.5f, 0.0f);

        if(directionFace == 1){
            directionVector = new Vector3(1.0f, 0.0f, 0.0f);
        }
        if(directionFace == 2){
            directionVector = new Vector3(0.0f, 1.0f, 0.0f);
            boxSize = new Vector3(0.5f, 1.0f, 0.0f);
        }
        if(directionFace == 3){
            directionVector = new Vector3(0.0f, -1.0f, 0.0f);
            boxSize = new Vector3(0.5f, 1.0f, 0.0f);
        }

        GameObject newSwordProjectile = Instantiate(swordProjectilePrefab, location, Quaternion.identity) as GameObject;
        newSwordProjectile.GetComponent<SpriteRenderer>().sprite = swordProjectileSprite;
        newSwordProjectile.GetComponent<BoxCollider>().size = boxSize;


        StartCoroutine(weapon.blink(newSwordProjectile));


        while(true){
            if(newSwordProjectile == null){
                break;
            }
            newSwordProjectile.GetComponent<Rigidbody>().velocity = directionVector * projectileSpeed;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void swordProjectileFinish(Vector3 location){
        GameObject newSwordProjectileUpLeft = Instantiate(swordProjectileFinishPrefab, location, Quaternion.identity) as GameObject;
        GameObject newSwordProjectileUpRight = Instantiate(swordProjectileFinishPrefab, location, Quaternion.identity) as GameObject;
        GameObject newSwordProjectileDownLeft = Instantiate(swordProjectileFinishPrefab, location, Quaternion.identity) as GameObject;
        GameObject newSwordProjectileDownRight = Instantiate(swordProjectileFinishPrefab, location, Quaternion.identity) as GameObject;

        newSwordProjectileUpLeft.GetComponent<SpriteRenderer>().sprite = GameController.instance.upLeftSwordProjectile;
        newSwordProjectileUpRight.GetComponent<SpriteRenderer>().sprite = GameController.instance.upRightSwordProjectile;
        newSwordProjectileDownLeft.GetComponent<SpriteRenderer>().sprite = GameController.instance.downLeftSwordProjectile;
        newSwordProjectileDownRight.GetComponent<SpriteRenderer>().sprite = GameController.instance.downRightSwordProjectile;

        newSwordProjectileUpLeft.GetComponent<Rigidbody>().velocity = new Vector3(-1.0f, 1.0f, 0.0f) * projectileSpeed/3;
        newSwordProjectileUpRight.GetComponent<Rigidbody>().velocity = new Vector3(1.0f, 1.0f, 0.0f) * projectileSpeed/3;
        newSwordProjectileDownLeft.GetComponent<Rigidbody>().velocity = new Vector3(-1.0f, -1.0f, 0.0f) * projectileSpeed/3;
        newSwordProjectileDownRight.GetComponent<Rigidbody>().velocity = new Vector3(1.0f, -1.0f, 0.0f) * projectileSpeed/3;
    }
}