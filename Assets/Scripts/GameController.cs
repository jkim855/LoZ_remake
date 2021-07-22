using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject player;
    public int activeObjectCount;
    public List<GameObject> activeObjects = new List<GameObject>();
    public CameraControl mainCamera;

    public Vector2 directionCombo;

    public Vector2 direction;

    public int directionFace = 2;
    // start facing up
    // 0 - left
    // 1 - right
    // 2 - up
    // 3 - down

    public bool canMove = true;

    public GameObject batDoor;

    public Sprite linkleft;
    public Sprite linkDown;
    public Sprite linkUp;
    public Sprite linkRight;
    public Sprite leftDoorLocked;
    public Sprite rightDoorLocked;
    public Sprite leftDoor;
    public Sprite rightDoor;
    public Sprite upDoorLeft;
    public Sprite upDoorRight;
    public Sprite downDoorLeft;
    public Sprite downDoorRight;

    public Sprite downSword;
    public Sprite upSword;
    public Sprite leftSword;
    public Sprite rightSword;

    public Sprite downSwordProjectile;
    public Sprite upSwordProjectile;
    public Sprite leftSwordProjectile;
    public Sprite rightSwordProjectile;

    public Sprite upLeftSwordProjectile;
    public Sprite upRightSwordProjectile;
    public Sprite downLeftSwordProjectile;
    public Sprite downRightSwordProjectile;

    public Sprite bomb;
    public Sprite arrow;
    public Sprite boomerang;
    public Sprite black;

    public GameObject boomerangPrefab;

    public Sprite explosionOne;
    public Sprite explosionTwo;
    public Image leftUI;

    public Image bombBlink;
    public Image screenFade;

    public GameObject batPrefab;
    public GameObject dragonDoor;

    public GameObject skeletonPrefab;
    public GameObject gelPrefab;
    public GameObject boomerPrefab;
    public GameObject dragonPrefab;

    public bool batKey = false;
    public bool firstSkelKey = false;
    public bool secondSkelKey = false;
    public bool waterKey = false;
    public bool handKey = false;
    public bool hasBigHeart = false;

    public GameObject bigHeart;
    public GameObject heart;
    public GameObject rupee;
    public GameObject bombDrop;
    public GameObject boomerangDrop;

    public GameObject portalGun;
    public Vector2 firstSkelKeyLoc = new Vector2(0.0f, 0.0f);

    public Vector2 secondSkelKeyLoc = new Vector2(0.0f, 0.0f);
    public GameObject keyPrefab;

    public Vector2 curRoom = new Vector2(0.0f, 0.0f);
    public AudioClip openDoor;
    
    public AudioClip swordSwing;
    public AudioClip swordFire;
    public AudioClip blockSound;
    public AudioClip bombPlace;
    public AudioClip bombBlow;
    public AudioClip arrowOneSound;
    public AudioClip arrowTwoSound;
    public AudioClip boomerangAudio;
    public AudioClip deathAudio;
    public AudioClip keyAudio;
    public AudioClip stunAudio;
    public AudioClip hurtAudio;
    public AudioClip playerHurtAudio;
    public AudioClip textAudio;
    public AudioClip findAudio;
    public GameObject oldManBlock;
    public GameObject bowBlock;

    public Sprite boomLeft;
    public Sprite boomLeftWalk;
    public Sprite boomUp;
    public Sprite boomUpWalk;
    public Sprite boomDown;
    public Sprite boomDownWalk;
    public Sprite boomRight;
    public Sprite boomRightWalk;
    public Sprite portalGunSprite;

    public Sprite leftPortalGun;
    public Sprite rightPortalGun;
    public Sprite upPortalGun;
    public Sprite downPortalGun;
    public Sprite orangePortalLeft;
    public Sprite orangePortalRight;
    public Sprite orangePortalUp;
    public Sprite orangePortalDown;

    public Sprite bluePortalLeft;
    public Sprite bluePortalRight;
    public Sprite bluePortalUp;
    public Sprite bluePortalDown;
    
    public Sprite bluePortalProjectile;
    public Sprite orangePortalProjectile;

    public GameObject portalPrefab;

    public GameObject Snorlax;
    public GameObject dartTrap;
    public GameObject fastBoi;
    public bool gotEndLoot = false;
    public bool midKey = false;

    public AudioClip portalTeleport;
    public AudioClip portalMake;
    public AudioClip portalShoot;
    public AudioClip rupee_collection_sound_clip;
    public AudioClip health_collection_sound_clip;
    public AudioClip key_collection_sound_clip;
    public AudioClip bomb_collection_sound_clip;

    public float handWait = 5.0f;
    public float curHandWait = 5.0f;
    public List<int> activeHands = new List<int>();

    public Dictionary<Vector2, List<GameObject>> objectsToSpawn = new Dictionary<Vector2, List<GameObject>>();
    

    public Dictionary<string, Sprite> replaceFloor = new Dictionary<string, Sprite>();


    public bool god_mode = false;
    void Awake(){
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Screen.SetResolution(256*4, 240*4, true);
        replaceFloor.Add("upLockedDoorLeft", upDoorLeft);
        replaceFloor.Add("upLockedDoorRight", upDoorRight);
        replaceFloor.Add("downLockedDoorLeft", downDoorLeft);
        replaceFloor.Add("downLockedDoorRight", downDoorRight);
        replaceFloor.Add("leftLockedDoor", leftDoor);
        replaceFloor.Add("rightLockedDoor", rightDoor);

        Vector2 oneLeft = new Vector2(-1, 0);
        Vector2 oneMiddle = new Vector2(0, 0);
        Vector2 oneRight = new Vector2(1, 0);

        Vector2 twoMiddle = new Vector2(0, 1);

        Vector2 threeLeft = new Vector2(-1, 2);
        Vector2 threeMiddle = new Vector2(0, 2);
        Vector2 threeRight = new Vector2(1, 2);

        Vector2 fourLeftLeft = new Vector2(-2, 3);
        Vector2 fourLeft = new Vector2(-1, 3);
        Vector2 fourMiddle = new Vector2(0, 3);
        Vector2 fourRight = new Vector2(1, 3);
        Vector2 fourRightRight = new Vector2(2, 3);
        
        Vector2 fiveMiddle = new Vector2(0, 4);
        Vector2 fiveRightRight = new Vector2(2, 4);
        Vector2 fiveRightRightRight = new Vector2(3, 4);
        
        Vector2 sixLeft = new Vector2(-1, 5);
        Vector2 sixMiddle = new Vector2(0, 5);

        Vector2 bowRoom = new Vector2(10, 10);

        Vector2 firstCustom = new Vector2(20, 20);
        Vector2 secondCustom = new Vector2(20, 21);
        Vector2 thirdCustom = new Vector2(20, 22);
        Vector3 fourthCustom = new Vector2(21, 22);

        objectsToSpawn.Add(firstCustom, new List<GameObject>());
        objectsToSpawn.Add(secondCustom, new List<GameObject>());
        objectsToSpawn.Add(thirdCustom, new List<GameObject>());
        objectsToSpawn.Add(fourthCustom, new List<GameObject>());

        
        objectsToSpawn.Add(bowRoom, new List<GameObject>());

        objectsToSpawn.Add(oneLeft, new List<GameObject>());
        objectsToSpawn.Add(oneMiddle, new List<GameObject>());
        objectsToSpawn.Add(oneRight, new List<GameObject>());

        objectsToSpawn.Add(twoMiddle, new List<GameObject>());

        objectsToSpawn.Add(threeLeft, new List<GameObject>());
        objectsToSpawn.Add(threeMiddle, new List<GameObject>());
        objectsToSpawn.Add(threeRight, new List<GameObject>());

        objectsToSpawn.Add(fourLeftLeft, new List<GameObject>());
        objectsToSpawn.Add(fourLeft, new List<GameObject>());
        objectsToSpawn.Add(fourMiddle, new List<GameObject>());
        objectsToSpawn.Add(fourRight, new List<GameObject>());
        objectsToSpawn.Add(fourRightRight, new List<GameObject>());

        objectsToSpawn.Add(fiveMiddle, new List<GameObject>());
        objectsToSpawn.Add(fiveRightRight, new List<GameObject>());
        objectsToSpawn.Add(fiveRightRightRight, new List<GameObject>());

        objectsToSpawn.Add(sixLeft, new List<GameObject>());
        objectsToSpawn.Add(sixMiddle, new List<GameObject>());

        objectsToSpawn[secondCustom].Add(Snorlax);
        objectsToSpawn[secondCustom].Add(dartTrap);

        objectsToSpawn[fourthCustom].Add(fastBoi);
        objectsToSpawn[fourthCustom].Add(fastBoi);
        objectsToSpawn[fourthCustom].Add(fastBoi);
        objectsToSpawn[fourthCustom].Add(fastBoi);
        objectsToSpawn[fourthCustom].Add(fastBoi);
        objectsToSpawn[fourthCustom].Add(fastBoi);
        objectsToSpawn[fourthCustom].Add(fastBoi);
        objectsToSpawn[fourthCustom].Add(fastBoi);

        objectsToSpawn[oneLeft].Add(batPrefab);
        objectsToSpawn[oneLeft].Add(batPrefab);
        objectsToSpawn[oneLeft].Add(batPrefab);

        objectsToSpawn[oneRight].Add(skeletonPrefab);
        objectsToSpawn[oneRight].Add(skeletonPrefab);
        objectsToSpawn[oneRight].Add(skeletonPrefab);
        objectsToSpawn[oneRight].Add(skeletonPrefab);
        objectsToSpawn[oneRight].Add(skeletonPrefab);

        objectsToSpawn[twoMiddle].Add(skeletonPrefab);
        objectsToSpawn[twoMiddle].Add(skeletonPrefab);
        objectsToSpawn[twoMiddle].Add(skeletonPrefab);
        // objectsToSpawn[twoMiddle].Add(boomerPrefab);

        objectsToSpawn[threeLeft].Add(batPrefab);
        objectsToSpawn[threeLeft].Add(batPrefab);
        objectsToSpawn[threeLeft].Add(batPrefab);
        objectsToSpawn[threeLeft].Add(batPrefab);
        objectsToSpawn[threeLeft].Add(batPrefab);
        objectsToSpawn[threeLeft].Add(batPrefab);

        objectsToSpawn[threeMiddle].Add(skeletonPrefab);
        objectsToSpawn[threeMiddle].Add(skeletonPrefab);
        objectsToSpawn[threeMiddle].Add(skeletonPrefab);
        objectsToSpawn[threeMiddle].Add(skeletonPrefab);
        objectsToSpawn[threeMiddle].Add(skeletonPrefab);

        objectsToSpawn[threeRight].Add(batPrefab);
        objectsToSpawn[threeRight].Add(batPrefab);
        objectsToSpawn[threeRight].Add(batPrefab);
        objectsToSpawn[threeRight].Add(batPrefab);
        objectsToSpawn[threeRight].Add(batPrefab);
        objectsToSpawn[threeRight].Add(batPrefab);
        objectsToSpawn[threeRight].Add(batPrefab);
        objectsToSpawn[threeRight].Add(batPrefab);

        objectsToSpawn[fourLeft].Add(gelPrefab);
        objectsToSpawn[fourLeft].Add(gelPrefab);
        objectsToSpawn[fourLeft].Add(gelPrefab);


        objectsToSpawn[fourMiddle].Add(gelPrefab);
        objectsToSpawn[fourMiddle].Add(gelPrefab);
        objectsToSpawn[fourMiddle].Add(gelPrefab);
        objectsToSpawn[fourMiddle].Add(gelPrefab);
        objectsToSpawn[fourMiddle].Add(gelPrefab);
 
        objectsToSpawn[fourRight].Add(boomerPrefab);
        objectsToSpawn[fourRight].Add(boomerPrefab);
        objectsToSpawn[fourRight].Add(boomerPrefab);

        objectsToSpawn[fiveMiddle].Add(skeletonPrefab);
        objectsToSpawn[fiveMiddle].Add(skeletonPrefab);
        objectsToSpawn[fiveMiddle].Add(skeletonPrefab);

        objectsToSpawn[fiveRightRight].Add(dragonPrefab);

        objectsToSpawn[sixMiddle].Add(boomerPrefab);
        objectsToSpawn[sixMiddle].Add(boomerPrefab);
        objectsToSpawn[sixMiddle].Add(boomerPrefab);

        objectsToSpawn[bowRoom].Add(batPrefab);
        objectsToSpawn[bowRoom].Add(batPrefab);
        objectsToSpawn[bowRoom].Add(batPrefab);
        objectsToSpawn[bowRoom].Add(batPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clearActives(){
        while(activeObjects.Count > 0){
            GameObject removeThis = activeObjects[0];
            if(removeThis != null && removeThis.tag == "boomerangProjectile" && removeThis.GetComponent<boomerangControl>().owner == GameController.instance.player){
                GameController.instance.player.GetComponent<PlayerThrowBoomerang>().canThrow = true;
            }
            if(removeThis != null && removeThis.tag == "portalProjectile"){
                GameController.instance.player.GetComponent<PlayerShootPortal>().canFire = true;
            }
            activeObjects.RemoveAt(0);
            if(removeThis != null){
                Destroy(removeThis);
            }
            GameController.instance.player.GetComponent<PlayerShootPortal>().orangeLoc = Vector3.zero;
            GameController.instance.player.GetComponent<PlayerShootPortal>().blueLoc = Vector3.zero;
        }
    }

    public void MonsterSpawn(){
        int listStart = 0;
        if(curRoom == new Vector2(1, 3)){
            if(objectsToSpawn[curRoom].Count == 0 && GameController.instance.player.GetComponent<Inventory>().hasBoomerangPrev == false){
                GameObject newBoom = Instantiate(GameController.instance.boomerangDrop, new Vector3(56f, 40f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                GameController.instance.activeObjects.Add(newBoom);
            }
        }
        if(curRoom == new Vector2(20, 20)){
            if(GameController.instance.player.GetComponent<Inventory>().hasPortalGunPrev == false){
                GameObject newPortalGun = Instantiate(GameController.instance.portalGun, new Vector3(92f, 3f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                GameController.instance.activeObjects.Add(newPortalGun);
            }
        }
        if(curRoom == new Vector2(-1, 0)){
            if(objectsToSpawn[curRoom].Count == 0 && batKey == false){
                GameObject newKey = Instantiate(keyPrefab, new Vector3(26f, 2f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                activeObjects.Add(newKey);
            }
        }
        if(curRoom == new Vector2(0, 5)){
            if(objectsToSpawn[curRoom].Count == 0 && waterKey == false){
                GameObject newKey = Instantiate(keyPrefab, new Vector3(40f, 62f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                activeObjects.Add(newKey);
            }
        }
        if(curRoom == new Vector2(0, 2)){
            if(objectsToSpawn[curRoom].Count == 0 && midKey == false){
                GameObject newKey = Instantiate(keyPrefab, new Vector3(40f, 29f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                activeObjects.Add(newKey);
            }
        }
        if(curRoom == new Vector2(2, 3)){
            if(handKey == false){
                GameObject newKey = Instantiate(keyPrefab, new Vector3(74f, 35f, 0.0f), Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                activeObjects.Add(newKey);
            }
        }
        if(curRoom == new Vector2(2, 4)){
            if(hasBigHeart == false && objectsToSpawn[curRoom].Count == 0){
                GameObject newHeart = Instantiate(bigHeart, new Vector3(76f, 49f, 0.0f), Quaternion.identity) as GameObject;
                activeObjects.Add(newHeart);
            }
        }

        if(curRoom == new Vector2(2, 4)){
            for(int q=listStart; q<objectsToSpawn[curRoom].Count; q++){
                GameObject newSpawn = Instantiate(objectsToSpawn[curRoom][q], new Vector3(74f, 49.5f, 0.0f), Quaternion.identity) as GameObject;
                activeObjects.Add(newSpawn);
            }
        }
        else if(curRoom == new Vector2(20, 21)){
            for(int q=listStart; q<objectsToSpawn[curRoom].Count; q++){
                Vector3 newSpawnLoc = Vector3.zero;
                if(objectsToSpawn[curRoom][q].tag == "snorlax"){
                    newSpawnLoc = new Vector3(87.5f, 19f, 0f);
                }
                else{
                    newSpawnLoc = new Vector3(82f, 15f, 0f);
                }
                
                GameObject newSpawn = Instantiate(objectsToSpawn[curRoom][q], newSpawnLoc, Quaternion.identity) as GameObject;
                activeObjects.Add(newSpawn);
            }
        }
        else if(curRoom == new Vector2(21, 22)){
            int[] xLoc = {25, 30, 29, 24, 30, 29, 25, 30};
            int start = 100;
            for(int q=listStart; q<objectsToSpawn[curRoom].Count; q++){
                Vector3 newSpawnLoc = new Vector3(start + q, xLoc[q], 0.0f);
                GameObject newSpawn = Instantiate(objectsToSpawn[curRoom][q], newSpawnLoc, Quaternion.identity) as GameObject;
                activeObjects.Add(newSpawn);
            }
            if(!gotEndLoot){
                GameObject newSpawn = Instantiate(rupee, new Vector3(108f, 27f, 0f), Quaternion.identity) as GameObject;
                activeObjects.Add(newSpawn);
            }
        }
        else{
            for(int q=listStart; q<objectsToSpawn[curRoom].Count; q++){
                GameObject newSpawn = Instantiate(objectsToSpawn[curRoom][q], new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0.0f), Quaternion.identity) as GameObject;
                activeObjects.Add(newSpawn);
            }
        }


        if(curRoom == new Vector2(1, 0)){
            if(!firstSkelKey){
                if(objectsToSpawn[curRoom].Count == 0){
                    GameObject newKey = Instantiate(keyPrefab, new Vector3(firstSkelKeyLoc.x, firstSkelKeyLoc.y, 0.0f), Quaternion.identity) as GameObject;
                    AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                    activeObjects.Add(newKey);
                }
                else{
                    activeObjects[0].GetComponent<lootPossession>().hasLoot = true;
                }
            }
        }
        if(curRoom == new Vector2(0, 4)){
            if(!secondSkelKey){
                if(objectsToSpawn[curRoom].Count == 0){
                    GameObject newKey = Instantiate(keyPrefab, new Vector3(secondSkelKeyLoc.x, secondSkelKeyLoc.y, 0.0f), Quaternion.identity) as GameObject;
                    AudioSource.PlayClipAtPoint(GameController.instance.keyAudio, Camera.main.transform.position);
                    activeObjects.Add(newKey);
                }
                else{
                    activeObjects[0].GetComponent<lootPossession>().hasLoot = true;
                }
            }
        }
    }
}
