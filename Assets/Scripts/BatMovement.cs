using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour {

	private Coroutine currMove;
	private Vector3[] directions = {Vector3.up, Vector3.right, Vector3.left, Vector3.down, Vector3.up + Vector3.right, Vector3.up + Vector3.left, Vector3.down + Vector3.left, Vector3.down + Vector3.right};
	private Vector3 prevDir;
	private Vector3 currDir;
	private Rigidbody rb;

    public int count = 0;

    public int restTerm = 30;

    public float waitTime = 2.0f;

	private Animator animator;

	private float unitMove = 0.5f;

	public bool canMove = true;

	float moveSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        count = Random.Range(0, Mathf.RoundToInt(restTerm / 2));
		animator = GetComponent<Animator>();
    	rb = GetComponent<Rigidbody>();
    	currDir = directions[Random.Range(0, directions.Length)];
        currMove = StartCoroutine(makeMove());
    }

	void findNewDir(){
		prevDir = currDir;
		currDir = directions[Random.Range(0, directions.Length)];
		while(currDir == prevDir || currDir == -1 * prevDir){
			currDir = directions[Random.Range(0, directions.Length)];
		}
	}

    void OnCollisionEnter(Collision other){
		if(!canMove){
			return;
		}
        findNewDir();
        StopCoroutine(currMove);
        animator.speed = 1.0f;
        currMove = StartCoroutine(makeMove());
    }

    IEnumerator makeMove(){
    	while(true){
			if(!canMove){
				yield return new WaitForEndOfFrame();
				continue;
			}
            if(count == restTerm){
                count = 0;
                animator.speed = 0.0f;
                rb.velocity = Vector3.zero;
                moveSpeed = 0.0f;
                yield return new WaitForSeconds(waitTime);
                animator.speed = 1.0f;
            }
            else if(count >= restTerm - 6 && moveSpeed > 0.5f){
                moveSpeed -= 0.5f;
            }
            if(moveSpeed < 3.0f && count < 6){
                moveSpeed += 0.5f;
            }
            for(float moved = 0; moved < unitMove; moved += moveSpeed * Time.deltaTime){
                while(!canMove){
					yield return null;
				}
                rb.velocity = currDir * moveSpeed;
                yield return null;
            }
		    findNewDir();
            count++;
		}
    }
}