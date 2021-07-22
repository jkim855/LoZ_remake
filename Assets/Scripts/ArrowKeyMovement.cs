using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour
{
    Rigidbody rb;
    public float movement_speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    
    Vector2 GetInput(){
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");
        Vector2 prevDirectionCombo = GameController.instance.directionCombo;
        Vector2 prevDirection = GameController.instance.direction;
        Vector2 curDirection = new Vector2(horizontal_input, vertical_input);
        if(curDirection == prevDirectionCombo){
            return GameController.instance.direction;
        }
        GameController.instance.directionCombo = curDirection;

        if (horizontal_input != 0 && vertical_input != 0){
            if(prevDirection.x != 0){
                GameController.instance.direction = new Vector2(0.0f, vertical_input);
            }
            else{
                GameController.instance.direction = new Vector2(horizontal_input, 0.0f);
            }
        }
        else{
            GameController.instance.direction = new Vector2(horizontal_input, vertical_input);
        }

        if(GameController.instance.direction.x > 0){
            GameController.instance.directionFace = 1;
        }
        else if(GameController.instance.direction.x < 0){
            GameController.instance.directionFace = 0;
        }
        else if (GameController.instance.direction.y > 0){
            GameController.instance.directionFace = 2;
        }
        else if (GameController.instance.direction.y < 0){
            GameController.instance.directionFace = 3;
        }

		float curr_y = transform.position.y;
		float curr_x = transform.position.x;
        if (GameController.instance.direction.y == 0 && GameController.instance.direction.x != 0){
        	curr_y = (float)(Mathf.Round(curr_y * 2.0f) / 2.0f);
        }
        else if(GameController.instance.direction.y != 0 && GameController.instance.direction.x == 0){
            curr_x = (float)(Mathf.Round(curr_x * 2.0f) / 2.0f);
        }
        transform.position = new Vector3(curr_x, curr_y, 0);
        return new Vector2(horizontal_input, vertical_input);
    }

    void Update()
    {
        if(!GameController.instance.canMove){
            return;
        }
        Vector2 current_input = GetInput();
        rb.velocity = current_input * movement_speed;
    }
}
