using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Camera mainCamera;
    
    float panTime = 3.0f;

    float appearTime = 0.5f;

    float width = 16.0f;
    float height = 10.92f;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    public void changeRoom(bool horizontal, int direction, Vector3 extraPlayerOffset){
        GameController.instance.clearActives();
        // -1 is down or left
        if(horizontal){
            GameController.instance.curRoom = new Vector2(GameController.instance.curRoom.x + direction, GameController.instance.curRoom.y);
        }
        else{
            Debug.Log("Y " + GameController.instance.curRoom.y);
            GameController.instance.curRoom = new Vector2(GameController.instance.curRoom.x, GameController.instance.curRoom.y + direction);
        }
        StartCoroutine(moveCamera(horizontal, direction));
        StartCoroutine(repositionPlayer(horizontal, direction, extraPlayerOffset));
    }

    IEnumerator moveCamera(bool horizontal, int direction){
        Vector3 startLocation = transform.position;
        Vector3 endLocation = transform.position;
        float start = 0.0f;
        float percentage = 0.0f;
        if(horizontal){
            endLocation.x += width * direction;
        }
        else{
            endLocation.y += height * direction;
        }

        while(percentage < 1.0f){
            start = Mathf.Min(Time.deltaTime + start, panTime);
            percentage = start/panTime;
            transform.position = Vector3.Lerp(startLocation, endLocation, percentage);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator repositionPlayer(bool horizontal, int direction, Vector3 extraPlayerOffset){
        GameController.instance.canMove = false;
        GameObject player = GameController.instance.player;
        player.GetComponent<BoxCollider>().enabled = false;
        Vector3 startLocation = player.transform.position;
        Vector3 endLocation = startLocation;
        float start = 0.0f;
        float percentage = 0.0f;
        if(horizontal){
            endLocation.x += 0.5f * direction;
        }
        else{
            endLocation.y += 0.5f * direction;
        }

        while(percentage < 1.0f){
            start = Mathf.Min(Time.deltaTime + start, appearTime);
            percentage = start/appearTime;
            player.transform.position = Vector3.Lerp(startLocation, endLocation, percentage);
            yield return new WaitForEndOfFrame();
        }

        startLocation = endLocation;
        yield return new WaitForSeconds(2.0f);
        start = 0.0f;
        percentage = 0.0f;

        if(horizontal){
            endLocation.x += 1.65f * direction;
        }
        else{
            if(direction == 1){
                endLocation.y += 1.5f;
            }
            else{
                endLocation.y -= 1.65f;
            }
        }

        endLocation += extraPlayerOffset;

        while(percentage < 1.0f){
            start = Mathf.Min(Time.deltaTime + start, appearTime);
            percentage = start/appearTime;
            player.transform.position = Vector3.Lerp(startLocation, endLocation, percentage);
            yield return new WaitForEndOfFrame();
        }
        GameController.instance.MonsterSpawn();
        yield return new WaitForEndOfFrame();
        player.GetComponent<BoxCollider>().enabled = true;
        GameController.instance.canMove = true;
    }
}
