using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMovement : MonoBehaviour
{

    private Coroutine currMove;
    private Coroutine moveAnimation;
    private Vector3[] directions = { Vector3.up, Vector3.right, Vector3.left, Vector3.down };
    private Vector3 prevDir;
    private Vector3 currDir;
    private Rigidbody rb;
    private RaycastHit hit;
    private int layerMask = ~((1 << 13) | (1 << 18) | (1 << 0));
    private bool blocked = false;
    //private List<Vector3> blockedDirs = new List<Vector3>();
    Sprite moveOne;
    Sprite moveTwo;

    public float waitTime = 0.0f;
    public bool canMove = true;

    public float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        findNewDir();
        currMove = StartCoroutine(makeMove());
        StartCoroutine(throwBoomerang());
    }

    IEnumerator findDir()
    {
        prevDir = currDir;
        currDir = directions[Random.Range(0, directions.Length)];

        
        while (currDir == prevDir || currDir == -1 * prevDir || Physics.Raycast(transform.position, transform.TransformDirection(currDir), out hit, 1f, layerMask))
        {

            currDir = directions[Random.Range(0, directions.Length)];
            yield return null;
        }
        //blockedDirs.Clear();
        if (moveAnimation != null)
        {
            StopCoroutine(moveAnimation);
        }
        moveAnimation = StartCoroutine(handleAnimation());
    }

    void findNewDir()
    {
        StartCoroutine(findDir());
    }

    void OnCollisionStay(Collision other)
    {
        if (!canMove)
        {
            return;
        }
        Vector3[] posToTry = new Vector3[3];
        if (currDir == Vector3.up || currDir == Vector3.down)
        {
            posToTry[0] = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            posToTry[1] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            posToTry[2] = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }
        else
        {
            posToTry[0] = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            posToTry[1] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            posToTry[2] = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
        if (!(Physics.Raycast(posToTry[0], transform.TransformDirection(currDir), out hit, 1f, layerMask) || Physics.Raycast(posToTry[1], transform.TransformDirection(currDir), out hit, 1f, layerMask) || Physics.Raycast(posToTry[2], transform.TransformDirection(currDir), out hit, 1f, layerMask)))
        {
            return;
        }
        else
        {
            blocked = true;
        }
        findNewDir();
        blocked = false;
    }

    IEnumerator throwBoomerang()
    {
        while (true)
        {
            if (!canMove)
            {
                yield return new WaitForSeconds(4f);
                continue;
            }
            canMove = false;
            GameObject newBoomerang = Instantiate(GameController.instance.boomerangPrefab, new Vector3(transform.position.x + currDir.x, transform.position.y + currDir.y, transform.position.z), Quaternion.identity) as GameObject;
            newBoomerang.GetComponent<boomerangControl>().owner = this.gameObject;
            newBoomerang.GetComponent<BoxCollider>().isTrigger = true;
            GameController.instance.activeObjects.Add(newBoomerang);
            StartCoroutine(newBoomerang.GetComponent<boomerangControl>().boomerangMovement(currDir));
        }
    }

    IEnumerator handleAnimation()
    {
        if (currDir == Vector3.right)
        {
            moveOne = GameController.instance.boomRight;
            moveTwo = GameController.instance.boomRightWalk;
        }
        if (currDir == Vector3.down)
        {
            moveOne = GameController.instance.boomDown;
            moveTwo = GameController.instance.boomDownWalk;
        }
        if (currDir == Vector3.left)
        {
            moveOne = GameController.instance.boomLeft;
            moveTwo = GameController.instance.boomLeftWalk;
        }
        if (currDir == Vector3.up)
        {
            moveOne = GameController.instance.boomUp;
            moveTwo = GameController.instance.boomUpWalk;
        }
        while (true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = moveOne;
            yield return new WaitForSeconds(0.2f);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = moveTwo;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator makeMove()
    {
        while (true)
        {
            if (!canMove)
            {
                yield return null;
                continue;
            }
            int randDist = Random.Range(1, 4);
            //Debug.DrawRay(transform.position, transform.TransformDirection(currDir) * 10, Color.white);
            for (int i = 0; i < randDist; i++)
            {
                if (!Physics.Raycast(transform.position, transform.TransformDirection(currDir), out hit, 1f, layerMask))
                {
                    for (float moved = 0; moved < 1; moved += moveSpeed * Time.deltaTime)
                    {
                        while (!canMove)
                        {
                            yield return null;
                        }
                        rb.velocity = currDir * moveSpeed;
                        yield return null;
                    }
                }
                else
                {
                    break;
                }
            }
            if (!blocked)
            {
                findNewDir();
                yield return null;
            }
            else
            {
                while (blocked)
                {
                    yield return null;
                }
            }

        }
    }
}