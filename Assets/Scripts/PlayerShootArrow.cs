using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootArrow : MonoBehaviour
{
	public bool canShoot = true;
	public GameObject arrowPrefab;

    public IEnumerator Shoot(){
        canShoot = false;
        float randoSound = Random.Range(0f, 1f);
        if(randoSound < 0.5){
            AudioSource.PlayClipAtPoint(GameController.instance.arrowOneSound, Camera.main.transform.position);
        }
        else{
            AudioSource.PlayClipAtPoint(GameController.instance.arrowTwoSound, Camera.main.transform.position);
        }
        GameController.instance.player.GetComponent<Inventory>().AddRupees(-1);
        GameController.instance.canMove = false;
        GameController.instance.player.GetComponent<Rigidbody>().velocity = new Vector2(0.0f, 0.0f);
        GameObject newArrow = Instantiate(arrowPrefab, GameController.instance.player.transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.2f);
        GameController.instance.canMove = true;
    }
}
