using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public static GameObject center = null;
    public static GameObject above = null;
    public static GameObject below = null;
    public float moveSpeed = 3;
    
    private Vector3 dir;
    private Rigidbody rb;
    private int pos;

    private void Awake()
    {
        if(center == null)
        {
            center = this.gameObject;
            pos = 1;
        }
        else if(above == null)
        {
            above = this.gameObject;
            pos = 0;
        }
        else if(below == null)
        {
            below = this.gameObject;
            pos = 2;
        }
        else if(center != this.gameObject && above != this.gameObject && below != this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("before adjust: " + dir);
        rb = GetComponent<Rigidbody>();
        dir = Vector3.Normalize(GameController.instance.player.transform.position - gameObject.transform.position);
        if (pos == 0)
        {
            dir += (Vector3.up * 0.3f);
        }
        if(pos == 1)
        {
            dir += (Vector3.down * 0.3f);
        }
        // Debug.Log("after: " + dir);
        StartCoroutine(makeMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(deathOrder());
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("WeakWall"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator deathOrder()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

    IEnumerator makeMove()
    {
        while (true)
        {
            for (float moved = 0; moved < 1; moved += moveSpeed * Time.deltaTime)
            {
                rb.velocity = dir * moveSpeed;
                yield return null;
            }
        }
    }
}
