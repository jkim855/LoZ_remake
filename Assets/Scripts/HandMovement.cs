using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public int pos;
    public float moveSpeed = 1.0f;
    public bool canMove = true;
    public Sprite handLeft;
    public Sprite handRight;
    public Vector3[] dirs;
    public AnimationClip faceRight;
    public AnimationClip faceLeft;
    public bool shouldDie = false;
    
    private Animator animator;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        dirs = new Vector3[3];
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);

        if (pos == 0)
        {
            dirs[0] = Vector3.down;
            dirs[1] = Vector3.right;
            dirs[2] = Vector3.up;
            GetComponent<SpriteRenderer>().sprite = handRight;
            transform.Rotate(Vector3.forward * 180);
        }
        if (pos == 1)
        {
            dirs[0] = Vector3.down;
            dirs[1] = Vector3.left;
            dirs[2] = Vector3.up;
            GetComponent<SpriteRenderer>().sprite = handLeft;
            transform.Rotate(Vector3.forward * 180);
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(faceRight, faceLeft));
            aoc.ApplyOverrides(anims);
            animator.runtimeAnimatorController = aoc;
        }
        if (pos == 2)
        {
            dirs[0] = Vector3.left;
            dirs[1] = Vector3.down;
            dirs[2] = Vector3.right;
            GetComponent<SpriteRenderer>().sprite = handRight;
            transform.Rotate(Vector3.forward * 90);
        }
        if (pos == 3)
        {
            dirs[0] = Vector3.left;
            dirs[1] = Vector3.up;
            dirs[2] = Vector3.right;
            GetComponent<SpriteRenderer>().sprite = handLeft;
            transform.Rotate(Vector3.forward * 90);
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(faceRight, faceLeft));
            aoc.ApplyOverrides(anims);
            animator.runtimeAnimatorController = aoc;
        }
        if (pos == 4)
        {
            dirs[0] = Vector3.up;
            dirs[1] = Vector3.left;
            dirs[2] = Vector3.down;
            GetComponent<SpriteRenderer>().sprite = handRight;
        }
        if (pos == 5)
        {
            dirs[0] = Vector3.up;
            dirs[1] = Vector3.right;
            dirs[2] = Vector3.down;
            GetComponent<SpriteRenderer>().sprite = handLeft;
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(faceRight, faceLeft));
            aoc.ApplyOverrides(anims);
            animator.runtimeAnimatorController = aoc;
        }
        if (pos == 6)
        {
            dirs[0] = Vector3.right;
            dirs[1] = Vector3.up;
            dirs[2] = Vector3.left;
            GetComponent<SpriteRenderer>().sprite = handRight;
            transform.Rotate(Vector3.forward * 270);
        }
        if (pos == 7)
        {
            dirs[0] = Vector3.right;
            dirs[1] = Vector3.down;
            dirs[2] = Vector3.left;
            GetComponent<SpriteRenderer>().sprite = handLeft;
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(faceRight, faceLeft));
            aoc.ApplyOverrides(anims);
            animator.runtimeAnimatorController = aoc;
            transform.Rotate(Vector3.forward * 270);
        }
        StartCoroutine(orderMoves());
    }

    IEnumerator orderMoves()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return StartCoroutine(makeMove(dirs[i], 3));
        }
        //Destroy(gameObject);
        shouldDie = true;
    }

    IEnumerator makeMove(Vector3 dir, int dist)
    {
        int count = 0;
        
        while(count < dist)
        {
            for (float moved = 0; moved < 1; moved += moveSpeed * Time.deltaTime)
            {
                while (!canMove)
                {
                    yield return null;
                }
                rb.velocity = dir * moveSpeed;
                yield return null;
            }
            count++;
            Debug.Log("count is: " + count);
        }
        Debug.Log("currCount: " + count);
    }
}
