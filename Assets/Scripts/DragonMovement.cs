using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{

    private Coroutine randMove;
    private Rigidbody rb;
    private Vector3[] dirs = { Vector3.left, Vector3.right };
    private Vector3 currDir;
    private int rng;
    private int currPos = 1;

    public AudioClip moveAudio;
    public bool canMove = true;
    public float moveSpeed = 0.5f;
    GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        randMove = StartCoroutine(makeMove());
        head = this.gameObject.transform.GetChild(0).gameObject;
    }

    void OnCollisionEnter(Collision other)
    {
        if(randMove != null){
            StopCoroutine(randMove);
        }
        randMove = StartCoroutine(makeMove());
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT PLAYER");
        }
    }

    IEnumerator makeMove()
    {
        while (true)
        {
            if (!canMove)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }
            rng = Random.Range(0, 2);
            if(currPos == 1)
            {
                rng = 1;
            }
            else if(currPos == 4)
            {
                rng = 0;
            }
            if(rng == 0)
            {
                currPos -= 1;
            }
            else
            {
                currPos++;
            }
            currDir = dirs[rng];
            AudioSource.PlayClipAtPoint(moveAudio, Camera.main.transform.position);
            for (float moved = 0; moved < 1; moved += moveSpeed * Time.deltaTime)
            {
                while (!canMove)
                {
                    yield return null;
                }
                rb.velocity = currDir * moveSpeed;
                if(head != null){
                    head.GetComponent<Rigidbody>().velocity = currDir * moveSpeed;
                }
                yield return null;
            }
        }
    }
}