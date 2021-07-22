using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    // Start is called before the first frame update
    Sprite startingSprite;
    void Start()
    {
        startingSprite = GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(spawnAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnAnimation(){
        if(this.gameObject.tag == "skeleton"){
            SkeletonMovement moveComponent = this.gameObject.GetComponent<SkeletonMovement>();
            moveComponent.canMove = false;
        }
        if(this.gameObject.tag == "boomerang"){
            BoomerangMovement moveComponent = this.gameObject.GetComponent<BoomerangMovement>();
            moveComponent.canMove = false;
        }
        if(this.gameObject.tag == "bat"){
            BatMovement moveComponent = this.gameObject.GetComponent<BatMovement>();
            moveComponent.canMove = false;
        }
        if(this.gameObject.tag == "gel"){
            GelMovement moveComponent = this.gameObject.GetComponent<GelMovement>();
            moveComponent.canMove = false;
        }
        if(this.gameObject.tag == "hand"){
            HandMovement moveComponent = this.gameObject.GetComponent<HandMovement>();
            moveComponent.canMove = false;
        }
        if(this.gameObject.tag == "dragon"){
            DragonMovement moveComponent = this.gameObject.GetComponent<DragonMovement>();
            moveComponent.canMove = false;
        }
        Animator hold = GetComponent<Animator>();
        if(hold != null){
            hold.enabled = false;
        }
        GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);

        GetComponent<SpriteRenderer>().sprite = GameController.instance.explosionOne;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().sprite = GameController.instance.explosionTwo;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().sprite = startingSprite;

        if(this.gameObject.tag == "skeleton"){
            SkeletonMovement moveComponent = this.gameObject.GetComponent<SkeletonMovement>();
            moveComponent.canMove = true;
        }
        if(this.gameObject.tag == "boomerang"){
            BoomerangMovement moveComponent = this.gameObject.GetComponent<BoomerangMovement>();
            moveComponent.canMove = true;
        }
        if(this.gameObject.tag == "bat"){
            BatMovement moveComponent = this.gameObject.GetComponent<BatMovement>();
            moveComponent.canMove = true;
        }
        if(this.gameObject.tag == "gel"){
            GelMovement moveComponent = this.gameObject.GetComponent<GelMovement>();
            moveComponent.canMove = true;
        }
        if(this.gameObject.tag == "hand"){
            HandMovement moveComponent = this.gameObject.GetComponent<HandMovement>();
            moveComponent.canMove = true;
        }
        if(this.gameObject.tag == "dragon"){
            DragonMovement moveComponent = this.gameObject.GetComponent<DragonMovement>();
            moveComponent.canMove = true;
        }
        if(hold != null){
            hold.enabled = true;
        }
    }
}
