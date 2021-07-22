using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileMovement : MonoBehaviour
{
    public float stayTime = 0.3f;
    float startTime = 0.0f;
    public bool canBeMoved = true;

    public GameObject doorToOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision other){
        if(other.gameObject.tag == "Player" && canBeMoved){
            if(GameController.instance.direction.y != 0 || GameController.instance.direction.x != 0){
                startTime += Time.deltaTime;
            }
            else{
                startTime = 0.0f;
            }

            if(startTime >= stayTime){
                Debug.Log("ok");
                canBeMoved = false;
                StartCoroutine(moveBlock(GameController.instance.directionFace));
            }
        }
    }

    void OnCollisionEnter(Collision other){
        startTime = 0.0f;
    }

    void OnCollisionExit(Collision other){
        startTime = 0.0f;
    }

    IEnumerator moveBlock(int directionFace){
        float start = 0.0f;
        float percentage = 0.0f;
        float appearTime = 0.5f;
        Vector3 startLocation = this.transform.position;
        Vector3 moveDirection = new Vector3(-1f, 0f, 0f);
        if(directionFace == 1){
            moveDirection = new Vector3(1f, 0f, 0f);
        }
        else if(directionFace == 2){
            moveDirection = new Vector3(0f, 1f, 0f);
        }
        else if (directionFace == 3){
            moveDirection = new Vector3(0f, -1f, 0f);
        }
        Vector3 endLocation = new Vector3(startLocation.x + moveDirection.x, startLocation.y + moveDirection.y, startLocation.z);
        while(percentage < 1.0f){
            start = Mathf.Min(Time.deltaTime + start, appearTime);
            percentage = start/appearTime;
            this.transform.position = Vector3.Lerp(startLocation, endLocation, percentage);
            yield return new WaitForEndOfFrame();
        }
        AudioSource.PlayClipAtPoint(GameController.instance.blockSound, Camera.main.transform.position);
        if(doorToOpen != null){
            doorToOpen.GetComponent<SpriteRenderer>().sprite = GameController.instance.leftDoor;
            doorToOpen.GetComponent<BoxCollider>().size = new Vector3(1f, 0.5f, 1f);
            doorToOpen.GetComponent<BoxCollider>().center = new Vector3(0f, 0.25f, 0f);
        }
    }

    public void resetDoor(bool noise){
        if(doorToOpen != null){
            if(noise){
                AudioSource.PlayClipAtPoint(GameController.instance.openDoor, Camera.main.transform.position);
            }
            doorToOpen.GetComponent<SpriteRenderer>().sprite = GameController.instance.leftDoorLocked;
            doorToOpen.GetComponent<BoxCollider>().center = new Vector3(0f, 0f, 0f);
            doorToOpen.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 0.2f);
        }
    }
}
