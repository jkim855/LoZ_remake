using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeMovement : MonoBehaviour
{

    private Vector3[] dirs = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
    private Coroutine detectCheck;
    private Coroutine comeBack;
    private Rigidbody rb;
    private Vector3 foundDir;
    private float moveSpeed = 4.5f;
    private float returnSpeed = -2.0f;
    private bool onWayBack = false;
    public float thickness = 2f;
    private bool returned = true;
    private bool reachedEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        detectCheck = StartCoroutine(detectPlayer());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("spikeStop"))
        {
            reachedEnd = true;
        }
        if (collision.CompareTag("cornerWall") && onWayBack)
        {
            if(comeBack != null){
                StopCoroutine(comeBack);
            }
            returned = true;
            rb.velocity = Vector3.zero;
            onWayBack = false;
            reachedEnd = false;
            detectCheck = StartCoroutine(detectPlayer());
        }
    }

    IEnumerator makeReturn()
    {
        bool printed = false;
        while (!returned)
        {
            if (!printed)
            {
                printed = true;
            }
            rb.velocity = foundDir * returnSpeed;
            yield return null;
        }
    }

    IEnumerator detectPlayer()
    {
        while (true)
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                int layerMask = (1 << 18) | (1 << 16);
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, thickness, transform.TransformDirection(dirs[i]), out hit, 10, layerMask))
                {
                    while (!reachedEnd)
                    {
                        if (!hit.collider.CompareTag("Player"))
                        {
                            break;
                        }
                        Debug.DrawRay(transform.position, transform.TransformDirection(dirs[i]) * hit.distance, Color.yellow);
                        foundDir = dirs[i];
                        returned = false;
                        rb.velocity = foundDir * moveSpeed;
                        yield return null;
                    }
                }
                //Debug.DrawRay(transform.position, transform.TransformDirection(dirs[i]) * 10, Color.white);
            }
            if (returned)
            {
                yield return null;
            }
            else
            {
                onWayBack = true;
                comeBack = StartCoroutine(makeReturn());
                while (onWayBack)
                {
                    yield return null;
                }
            }
        }
    }
}
