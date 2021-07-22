using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelMovement : MonoBehaviour
{

    private Coroutine currMove;
    private Vector3[] directions = { Vector3.up, Vector3.right, Vector3.left, Vector3.down };
    private Vector3 prevDir;
    private Vector3 currDir;
    private Rigidbody rb;
    private float initMoveSpeed;

    private int count = 0;

    public int restTerm;

    public float waitTime = 2.0f;

    private float unitMove = 1.0f;

    public bool canMove = true;

    float moveSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        initMoveSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
        currDir = directions[Random.Range(0, directions.Length)];
        currMove = StartCoroutine(makeMove());
    }

    void findNewDir()
    {
        prevDir = currDir;
        currDir = directions[Random.Range(0, directions.Length)];
        while (currDir == prevDir || currDir == -1 * prevDir)
        {
            currDir = directions[Random.Range(0, directions.Length)];
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!canMove)
        {
            return;
        }
        findNewDir();
        StopCoroutine(currMove);
        currMove = StartCoroutine(makeMove());
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
            if (count >= restTerm)
            {
                count = 0;
                rb.velocity = Vector3.zero;
                moveSpeed = 0.0f;
                yield return new WaitForSeconds(waitTime);
            }
            moveSpeed = initMoveSpeed;
            for (float moved = 0; moved < unitMove; moved += moveSpeed * Time.deltaTime)
            {
                while(!canMove){
					yield return null;
				}
                rb.velocity = currDir * moveSpeed;
                yield return null;
            }
            findNewDir();
            count++;
            restTerm = Random.Range(1, 4);
        }
    }
}