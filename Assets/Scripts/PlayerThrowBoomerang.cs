using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowBoomerang : MonoBehaviour
{
    public bool canThrow = true;
    public GameObject boomerangPrefab;


    public IEnumerator Throw(){
        Debug.Log("throw");
        canThrow = false;
        GameController.instance.canMove = false;
        GameController.instance.player.GetComponent<Rigidbody>().velocity = new Vector2(0.0f, 0.0f);

        int directionFace = GameController.instance.directionFace;
        Vector3 directionVector = new Vector3(-1.0f, 0.0f, 0.0f);

        if(directionFace == 1){
            directionVector = new Vector3(1.0f, 0.0f, 0.0f);
        }
        if(directionFace == 2){
            directionVector = new Vector3(0.0f, 1.0f, 0.0f);
        }
        if(directionFace == 3){
            directionVector = new Vector3(0.0f, -1.0f, 0.0f);
        }

        GameObject newBoomerang = Instantiate(boomerangPrefab, GameController.instance.player.transform.position + directionVector, Quaternion.identity) as GameObject;
        GameController.instance.activeObjects.Add(newBoomerang);
        Vector3 combo = GameController.instance.directionCombo;
        if(combo.x != 0 && combo.y != 0){
            directionVector = combo.normalized;
        }
        newBoomerang.GetComponent<boomerangControl>().owner = this.gameObject;
        StartCoroutine(newBoomerang.GetComponent<boomerangControl>().boomerangMovement(directionVector));
        yield return new WaitForSeconds(0.2f);
        GameController.instance.canMove = true;
    }
}
