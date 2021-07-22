using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerPlaceBomb : MonoBehaviour
{
    public bool canPlace = true;
    public GameObject bombPrefab;
    public GameObject explosionPrefab;
    weaponSwitch weapon;

    void Start(){
        weapon = GetComponent<weaponSwitch>();
    }

    public IEnumerator Place(){
        canPlace = false;
        
        AudioSource.PlayClipAtPoint(GameController.instance.bombPlace, Camera.main.transform.position);

        GameController.instance.player.GetComponent<Inventory>().AddBombs(-1);
        GameController.instance.canMove = false;
        GameController.instance.player.GetComponent<Rigidbody>().velocity = new Vector2(0.0f, 0.0f);

        if(GameController.instance.player.GetComponent<Inventory>().bomb_count == 0){
            if(GameController.instance.player.GetComponent<Inventory>().hasBoomerang){
                GameController.instance.leftUI.sprite = GameController.instance.boomerang;
            }
            else if(GameController.instance.player.GetComponent<Inventory>().hasBow){
                GameController.instance.leftUI.sprite = GameController.instance.arrow;
            }
            else{
                GameController.instance.leftUI.sprite = GameController.instance.black;
            }
        }

        Vector3 bombLocation = GameController.instance.player.transform.position;
        if(GameController.instance.directionFace == 0){
            bombLocation -= new Vector3(1.0f, 0.0f, 0.0f);
            bombLocation += new Vector3(0.0f, 0.25f, 0.0f);
        }
        else if(GameController.instance.directionFace == 1){
            bombLocation += new Vector3(1.0f, 0.0f, 0.0f);
            bombLocation += new Vector3(0.0f, 0.25f, 0.0f);
        }
        else if(GameController.instance.directionFace == 2){
            bombLocation += new Vector3(0.0f, 1.25f, 0.0f);
        }
        else if(GameController.instance.directionFace == 3){
            bombLocation -= new Vector3(0.0f, 0.75f, 0.0f);
        }


        GameObject newBomb = Instantiate(bombPrefab, bombLocation, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.2f);
        GameController.instance.canMove = true;
    }

    public void bombExplosion(Vector3 location){
        GameObject newExplosion = Instantiate(explosionPrefab, location, Quaternion.identity) as GameObject;
        GameObject newExplosionLeft = Instantiate(explosionPrefab, new Vector3(location.x - 1, location.y, location.z), Quaternion.identity) as GameObject;
        GameObject newExplosionRight = Instantiate(explosionPrefab, new Vector3(location.x + 1, location.y, location.z), Quaternion.identity) as GameObject;
        GameObject newExplosionDownLeft = Instantiate(explosionPrefab, new Vector3(location.x - 0.5f, location.y - 1, location.z), Quaternion.identity) as GameObject;
        GameObject newExplosionUpLeft = Instantiate(explosionPrefab, new Vector3(location.x - 0.5f, location.y + 1, location.z), Quaternion.identity) as GameObject;
        GameObject newExplosionDownRight = Instantiate(explosionPrefab, new Vector3(location.x + 0.5f, location.y - 1, location.z), Quaternion.identity) as GameObject;
        GameObject newExplosionUpRight = Instantiate(explosionPrefab, new Vector3(location.x + 0.5f, location.y + 1, location.z), Quaternion.identity) as GameObject;

        var explosionColor = newExplosion.GetComponent<Renderer>().material.color;
        newExplosionLeft.GetComponent<Renderer>().material.color = new Color(explosionColor.r, explosionColor.g, explosionColor.g, 0.5f);
        newExplosionRight.GetComponent<Renderer>().material.color = new Color(explosionColor.r, explosionColor.g, explosionColor.g, 0.5f);
        newExplosionDownLeft.GetComponent<Renderer>().material.color = new Color(explosionColor.r, explosionColor.g, explosionColor.g, 0.5f);
        newExplosionUpLeft.GetComponent<Renderer>().material.color = new Color(explosionColor.r, explosionColor.g, explosionColor.g, 0.5f);
        newExplosionDownRight.GetComponent<Renderer>().material.color = new Color(explosionColor.r, explosionColor.g, explosionColor.g, 0.5f);
        newExplosionUpRight.GetComponent<Renderer>().material.color = new Color(explosionColor.r, explosionColor.g, explosionColor.g, 0.5f);

        StartCoroutine(bombAnimation(newExplosion));
        StartCoroutine(bombAnimation(newExplosionLeft));
        StartCoroutine(bombAnimation(newExplosionRight));
        StartCoroutine(bombAnimation(newExplosionDownLeft));
        StartCoroutine(bombAnimation(newExplosionUpLeft));
        StartCoroutine(bombAnimation(newExplosionDownRight));
        StartCoroutine(bombAnimation(newExplosionUpRight));
        StartCoroutine(bombBlink());
    }

    IEnumerator bombAnimation(GameObject explosion){
        StartCoroutine(weapon.blink(explosion));
        yield return new WaitForSeconds(0.1f);
        explosion.GetComponent<SpriteRenderer>().sprite = GameController.instance.explosionTwo;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator bombBlink(){
        GameController.instance.bombBlink.color = new Color(255, 255, 255, 0.25f);
        yield return new WaitForSeconds(0.1f);
        GameController.instance.bombBlink.color = new Color(255, 255, 255, 0.5f);
        yield return new WaitForSeconds(0.1f);
        GameController.instance.bombBlink.color = new Color(255, 255, 255, 0.0f);
    }
}
