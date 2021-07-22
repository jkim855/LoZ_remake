using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMovement : MonoBehaviour
{
    Inventory playerInventory;
    private List <GameObject> curCollisions = new List<GameObject>();

    public GameObject bowBlock;
    public GameObject oldBlock;


    public Text oldText;

    public GameObject firePrefab;
    public GameObject oldManPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "toLeft"){
            GameController.instance.mainCamera.changeRoom(true, -1, Vector3.zero);
        }
        else if(collision.gameObject.tag == "toRight"){
            GameController.instance.mainCamera.changeRoom(true, 1, Vector3.zero);
        }
        else if(collision.gameObject.tag == "toUp"){
            GameController.instance.mainCamera.changeRoom(false, 1, Vector3.zero);
        }
        else if(collision.gameObject.tag == "toDown"){
            GameController.instance.mainCamera.changeRoom(false, -1, Vector3.zero);
        }
        else if(collision.gameObject.tag == "bowTeleport"){
            GameController.instance.clearActives();
            GameController.instance.curRoom = new Vector2(10, 10);
            StartCoroutine(moveBowPlayer());
            StartCoroutine(fadeScreen());
        }
        else if(collision.gameObject.tag == "bowLeave"){
            GameController.instance.clearActives();
            GameController.instance.curRoom = new Vector2(-1, 5);
            GameController.instance.MonsterSpawn();
            StartCoroutine(leaveBowPlayer());
            StartCoroutine(fadeScreen());
        }
        else if(collision.gameObject.tag == "toLeftBow"){
            GameController.instance.bowBlock.transform.position = new Vector3(22f, 60f, 0f);
            GameController.instance.bowBlock.GetComponent<tileMovement>().canBeMoved = true;
            GameController.instance.mainCamera.changeRoom(true, -1, Vector3.zero);
        }
        else if(collision.gameObject.tag == "toRightOld"){
            GameController.instance.mainCamera.changeRoom(true, 1, new Vector3(1f, 0f, 0f));
            StartCoroutine(resetOld());
        }
        else if(collision.gameObject.tag == "toLeftOld"){
            GameController.instance.mainCamera.changeRoom(true, -1, Vector3.zero);
            StartCoroutine(handleOldMan());
        }
        else if(collision.gameObject.tag == "toLeftOldBlock"){
            GameController.instance.mainCamera.changeRoom(true, -1, Vector3.zero); 
            if(GameController.instance.objectsToSpawn[new Vector2(-1, 3)].Count == 0){
                oldBlock.GetComponent<tileMovement>().canBeMoved = true;
                oldBlock.GetComponent<tileMovement>().resetDoor(false);  
                oldBlock.transform.position = new Vector3(23f, 38f, 0.0f);
            } 
        }
        else if(collision.gameObject.tag == "toUpOldBlock"){
            GameController.instance.mainCamera.changeRoom(false, 1, Vector3.zero);  
            if(GameController.instance.objectsToSpawn[new Vector2(-1, 3)].Count == 0){
                oldBlock.GetComponent<tileMovement>().canBeMoved = true;
                oldBlock.GetComponent<tileMovement>().resetDoor(false); 
                oldBlock.transform.position = new Vector3(23f, 38f, 0.0f);
            } 
        }
        else if(collision.gameObject.tag == "toLeftBats"){
            GameController.instance.mainCamera.changeRoom(true, -1, new Vector3(-1f, 0f, 0f));
            StartCoroutine(handleBatLock());
        }
        else if(collision.gameObject.tag == "leftLockedDoor" || collision.gameObject.tag == "rightLockedDoor"
                || collision.gameObject.tag == "upLockedDoorLeft" || collision.gameObject.tag == "upLockedDoorRight"
                || collision.gameObject.tag == "downLockedDoorLeft" || collision.gameObject.tag == "downLockedDoorRight"){
            curCollisions.Add(collision.gameObject);
        }
    }

    IEnumerator handleOldMan(){
        oldText.text = "";
        while(!GameController.instance.canMove){
            yield return new WaitForEndOfFrame();
        }
        GameController.instance.canMove = false;
        GameController.instance.player.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        yield return new WaitForEndOfFrame();
        GameController.instance.player.GetComponent<Animator>().SetFloat("horizontal_input", 0);
        GameController.instance.player.GetComponent<Animator>().SetFloat("vertical_input", 0);
        GameController.instance.player.GetComponent<Animator>().speed = 0.0f;
        // SPAWN OLD MAN AND FIRE HERE push to actives
        GameObject oldMan = Instantiate(oldManPrefab, new Vector3(7f, 39f, 0f), Quaternion.identity) as GameObject;
        GameObject fireLeft = Instantiate(firePrefab, new Vector3(10f, 39f, 0f), Quaternion.identity) as GameObject;
        GameObject fireRight = Instantiate(firePrefab, new Vector3(4f, 39f, 0f), Quaternion.identity) as GameObject;
        GameController.instance.activeObjects.Add(oldMan);
        GameController.instance.activeObjects.Add(fireLeft);
        GameController.instance.activeObjects.Add(fireRight);
        string message = "YEET OR BE YEETED";
        for(int q=0; q<message.Length; q++){
            oldText.text += message[q];
            AudioSource.PlayClipAtPoint(GameController.instance.textAudio, Camera.main.transform.position);
            yield return new WaitForSeconds(0.2f);
        }
        GameController.instance.canMove = true;

    }

    IEnumerator resetOld(){
        GameController.instance.clearActives();
        oldText.text = "";
        oldBlock.transform.position = new Vector3(23f, 38f, 0.0f);
        oldBlock.GetComponent<tileMovement>().canBeMoved = true;
        yield return new WaitForSeconds(3f);
        oldBlock.GetComponent<tileMovement>().resetDoor(true);
    }

    IEnumerator handleBatLock(){
        yield return new WaitForSeconds(3f);
        if(GameController.instance.objectsToSpawn[new Vector2(-1, 2)].Count > 0){
            AudioSource.PlayClipAtPoint(GameController.instance.openDoor, Camera.main.transform.position);
            GameController.instance.batDoor.GetComponent<SpriteRenderer>().sprite = GameController.instance.rightDoorLocked;
            GameController.instance.batDoor.GetComponent<BoxCollider>().size = new Vector3(1f, 2f, 1f);
        }
    }

    IEnumerator moveBowPlayer(){
        yield return new WaitForSeconds(0.3f);
        GameController.instance.mainCamera.transform.position = new Vector3(39.5f, 75.5f, GameController.instance.mainCamera.transform.position.z);
        GameController.instance.player.transform.position = new Vector3(35f, 79f, GameController.instance.player.transform.position.z);
        float start = 0.0f;
        float percentage = 0.0f;
        Vector3 endLocation = new Vector3(GameController.instance.player.transform.position.x, GameController.instance.player.transform.position.y - 2, GameController.instance.player.transform.position.z);
        float appearTime = 0.5f;
        Vector3 startLocation = GameController.instance.player.transform.position;

        GameController.instance.player.GetComponent<Animator>().SetFloat("horizontal_input", 0);
        GameController.instance.player.GetComponent<Animator>().SetFloat("vertical_input", -1);

        while(percentage < 1.0f){
            start = Mathf.Min(Time.deltaTime + start, appearTime);
            percentage = start/appearTime;
            GameController.instance.player.transform.position = Vector3.Lerp(startLocation, endLocation, percentage);
            yield return new WaitForEndOfFrame();
        }

        GameController.instance.player.GetComponent<Animator>().SetFloat("horizontal_input", 0);
        GameController.instance.player.GetComponent<Animator>().SetFloat("vertical_input", 0);

    }

    IEnumerator leaveBowPlayer(){
        yield return new WaitForSeconds(0.3f);
        GameController.instance.mainCamera.transform.position = new Vector3(23.5f, 62.464f, GameController.instance.mainCamera.transform.position.z);
        GameController.instance.player.transform.position = new Vector3(22, 58, GameController.instance.player.transform.position.z);
        GameController.instance.player.GetComponent<Animator>().SetFloat("horizontal_input", 0);
        GameController.instance.player.GetComponent<Animator>().SetFloat("vertical_input", -1);
        bowBlock.transform.position = new Vector3(22f, 60f, 0f);
        bowBlock.GetComponent<tileMovement>().canBeMoved = true;
        yield return new WaitForEndOfFrame();
        GameController.instance.player.GetComponent<Animator>().SetFloat("horizontal_input", 0);
        GameController.instance.player.GetComponent<Animator>().SetFloat("vertical_input", 0);
    }

    public IEnumerator fadeScreen(){
        GameController.instance.canMove = false;
        GameController.instance.player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        float timeBetweenFades = 0.1f;
        GameController.instance.screenFade.color = new Color(0, 0, 0, 0.25f);
        yield return new WaitForSeconds(timeBetweenFades);
        GameController.instance.screenFade.color = new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(timeBetweenFades);
        GameController.instance.screenFade.color = new Color(0, 0, 0, 0.75f);
        yield return new WaitForSeconds(timeBetweenFades);
        GameController.instance.screenFade.color = new Color(0, 0, 0, 1f);

        
        yield return new WaitForSeconds(0.1f);

        GameController.instance.screenFade.color = new Color(0, 0, 0, 0.75f);
        yield return new WaitForSeconds(timeBetweenFades);
        GameController.instance.screenFade.color = new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(timeBetweenFades);
        GameController.instance.screenFade.color = new Color(0, 0, 0, 0.25f);
        yield return new WaitForSeconds(timeBetweenFades);
        GameController.instance.screenFade.color = new Color(0, 0, 0, 0f);

        GameController.instance.canMove = true;
        GameController.instance.MonsterSpawn();
    }

    void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag == "leftLockedDoor" || collision.gameObject.tag == "rightLockedDoor"
            || collision.gameObject.tag == "upLockedDoorLeft" || collision.gameObject.tag == "upLockedDoorRight"
            || collision.gameObject.tag == "downLockedDoorLeft" || collision.gameObject.tag == "downLockedDoorRight"){
            curCollisions.Remove(collision.gameObject);
        }
    }

    void OnCollisionStay(Collision collisionInfo){
        if(curCollisions.Count > 0 && playerInventory.key_count > 0){
            if(curCollisions.Count == 1 && (curCollisions[0].tag != "leftLockedDoor" && curCollisions[0].tag != "rightLockedDoor")){
                return;
            }
            GameObject first = curCollisions[0];
            GameObject second;

            if(curCollisions.Count == 2){
                if(first.tag == "upLockedDoorLeft" || first.tag == "upLockedDoorRight"){
                    if(Input.GetAxisRaw("Vertical") <= 0){
                        return;
                    }
                }
                else{
                    if(Input.GetAxisRaw("Vertical") >= 0){
                        return;
                    }
                }

                second = curCollisions[1];
                if(first.transform.position.x > second.transform.position.x){
                    GameObject temp = first;
                    first = second;
                    second = temp;
                }
                
                first.GetComponent<SpriteRenderer>().sprite = GameController.instance.replaceFloor[first.tag];
                second.GetComponent<SpriteRenderer>().sprite = GameController.instance.replaceFloor[second.tag];
                first.GetComponent<BoxCollider>().enabled = false;
                second.GetComponent<BoxCollider>().enabled = false;
                AudioSource.PlayClipAtPoint(GameController.instance.openDoor, Camera.main.transform.position);
                curCollisions.Remove(first);
                curCollisions.Remove(second);
            }
            else{
                if(first.tag == "leftLockedDoor"){
                    if(Input.GetAxisRaw("Horizontal") >= 0){
                        return;
                    }
                }
                else{
                    if(Input.GetAxisRaw("Horizontal") <= 0){
                        return;
                    }
                }
                first.GetComponent<SpriteRenderer>().sprite = GameController.instance.replaceFloor[first.tag];
                Debug.Log("update");
                first.GetComponent<BoxCollider>().size = new Vector3(1f, 0.5f, 0.2f);
                AudioSource.PlayClipAtPoint(GameController.instance.openDoor, Camera.main.transform.position);
                curCollisions.Remove(first);
            }
            playerInventory.AddKey(-1);
        }
    }
}
