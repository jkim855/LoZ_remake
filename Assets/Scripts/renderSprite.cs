using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderSprite : MonoBehaviour
{
	public Sprite toLeftSprite;
	public Sprite toRightSprite;
	public Sprite toUpSprite;
	public Sprite toDownSprite;
    public float flySpeed = 20f;
    

    private Rigidbody rb;
    public Vector3 flyDir;
    private float startTime;
    private PlayerShootArrow PlayerShootArrowScript;
    private float flyDur = 1f;
    private BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        startTime = Time.time;
        PlayerShootArrowScript = GameController.instance.player.GetComponent<PlayerShootArrow>();
    	int directionFace = GameController.instance.directionFace;
        PlayerShootArrowScript.canShoot = false;
        if(directionFace == 0){
        //left
            GetComponent<SpriteRenderer>().sprite = toLeftSprite;
            flyDir = Vector3.left;
            collider.size = new Vector3(1f, 0.3f, 0f);
        }
        else if(directionFace == 1){
        //right
            GetComponent<SpriteRenderer>().sprite = toRightSprite;
            flyDir = Vector3.right;
            collider.size = new Vector3(1f, 0.3f, 0f);
        }
        else if(directionFace == 2){
        // up
			GetComponent<SpriteRenderer>().sprite = toUpSprite;
            flyDir = Vector3.up;
            collider.size = new Vector3(0.3f, 1f, 0f);
        }
        else if(directionFace == 3){
        // down
			GetComponent<SpriteRenderer>().sprite = toDownSprite;
            flyDir = Vector3.down;
            collider.size = new Vector3(0.3f, 1f, 0f);
        }
    }

    // Update is called once per frame
    void Update(){
        rb.velocity = flyDir * flySpeed;
    }
}
