using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFastBoi : MonoBehaviour
{
    Rigidbody rb;
    private Vector3 currDir;
    private Coroutine moveCoroutine;

    public int firDir = 1;
    public float moveSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(firDir == 1)
        {
            currDir = Vector3.down;
        }
        else
        {
            currDir = Vector3.up;
        }
        moveCoroutine = StartCoroutine(makeMove());
    }

    // Update is called once per frame
    void Update()
    {
        rb.transform.Rotate(0.0f, 0.0f, 15f);
    }

    void OnCollisionEnter()
    {
        StopCoroutine(moveCoroutine);
        currDir = -1 * currDir;
        moveCoroutine = StartCoroutine(makeMove());
    }

    IEnumerator makeMove()
    {
        while (true)
        {
            for (float moved = 0; moved < 1; moved += moveSpeed * Time.deltaTime)
            {
                rb.velocity = currDir * moveSpeed;
                yield return null;
            }
            yield return null;
        }
    }
}
