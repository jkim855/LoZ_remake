using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.instance.canMove){
            return;
        }
        Vector2 direction = GameController.instance.direction;
        animator.SetFloat("horizontal_input", direction.x);
        animator.SetFloat("vertical_input", direction.y);

        if(direction.x == 0 && direction.y == 0 && GameController.instance.player.GetComponent<PlayerAttack>().canAttack){
            animator.speed = 0.0f;
        }
        else{
            animator.speed = 1.0f;
        }
    }
}
