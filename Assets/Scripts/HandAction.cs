using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAction : MonoBehaviour
{
    private Animator animator;
    private bool gotPlayer = false;
    bool hasDied = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<HandMovement>().shouldDie) {
            if (gotPlayer && !hasDied)
            {
                hasDied = true;
                StartCoroutine(die());
            }
            else if(!gotPlayer){
                GameController.instance.activeHands.Remove(GetComponent<HandMovement>().pos);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.speed = 0.0f;
            gotPlayer = true;
            GameController.instance.canMove = false;
            GameController.instance.player.GetComponent<BoxCollider>().enabled = false;
            GameController.instance.player.GetComponent<Animator>().speed = 0.0f;
            StartCoroutine(orderManager());
        }
    }

    IEnumerator orderManager()
    {
        yield return StartCoroutine(dragPlayer());
    }

    IEnumerator dragPlayer()
    {
        while (!GetComponent<HandMovement>().shouldDie)
        {
            GameController.instance.player.transform.position = gameObject.transform.position;
            yield return null;

        }
    }

    IEnumerator die()
    {
        GameController.instance.clearActives();
        GameController.instance.curRoom = new Vector2(0, 0);
        StartCoroutine(GameController.instance.player.GetComponent<RoomMovement>().fadeScreen());
        GameController.instance.player.transform.position = new Vector3(39.5f, 1f, 0f);
        yield return new WaitForSeconds(0.35f);
        GameController.instance.player.GetComponent<BoxCollider>().enabled = true;
        GameController.instance.mainCamera.transform.position = new Vector3(39.5f, 7.5f, -10f);
        yield return new WaitForSeconds(0.35f);
        GameController.instance.player.GetComponent<Animator>().SetFloat("vertical_input", 1);
        yield return new WaitForSeconds(1f);
        GameController.instance.activeHands.Remove(GetComponent<HandMovement>().pos);
        Destroy(gameObject);
        yield return null;
    }

}
