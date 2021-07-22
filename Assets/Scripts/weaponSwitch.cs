using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponSwitch : MonoBehaviour
{

    PlayerAttack swordAttack;
    PlayerShootArrow arrowAttack;
    PlayerPlaceBomb bombAttack;
    // Start is called before the first frame update
    PlayerThrowBoomerang boomerangAttack;
    PlayerShootPortal portalAttack;
    public Image leftUI;
    public int curWep = -1;
    // 0 = bow
    // 1 = bomb
    // 2 = boomerang

    public bool inCustom = false;

    void Start()
    {
        swordAttack = GetComponent<PlayerAttack>();
        arrowAttack = GetComponent<PlayerShootArrow>();
        bombAttack = GetComponent<PlayerPlaceBomb>();
        boomerangAttack = GetComponent<PlayerThrowBoomerang>();
        portalAttack = GetComponentInParent<PlayerShootPortal>();
        leftUI = GameController.instance.leftUI;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)){
            if(GameController.instance.player.GetComponent<Inventory>().hasBow){
                leftUI.sprite = GameController.instance.arrow;
                curWep = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.J)){
            if(GameController.instance.player.GetComponent<Inventory>().bomb_count > 0){
                leftUI.sprite = GameController.instance.bomb;
                curWep = 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.K)){
            if(GameController.instance.player.GetComponent<Inventory>().hasBoomerang){
                leftUI.sprite = GameController.instance.boomerang;
                curWep = 2;
            }
        }

        if(Input.GetKeyDown(KeyCode.L)){
            if(GameController.instance.player.GetComponent<Inventory>().hasPortalGun){
                leftUI.sprite = GameController.instance.portalGunSprite;
                curWep = 3;
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            if(!GameController.instance.canMove){
                return;
            }
            if(inCustom){
                inCustom = false;
                StartCoroutine(goToNormal());
            }
            else{
                inCustom = true;
                StartCoroutine(goToCustom());
            }
        }

		if (Input.GetKeyDown(KeyCode.X) && swordAttack.canAttack && GameController.instance.canMove && !inCustom) {
			StartCoroutine(swordAttack.Attack());
		}

        if (Input.GetKeyDown(KeyCode.Z) && curWep == 0 && arrowAttack.canShoot && GameController.instance.canMove  && !inCustom && GameController.instance.player.GetComponent<Inventory>().rupee_count > 0) {
			StartCoroutine(arrowAttack.Shoot());
		}

        if (Input.GetKeyDown(KeyCode.Z) && curWep == 1 && bombAttack.canPlace && GameController.instance.canMove  && !inCustom && GameController.instance.player.GetComponent<Inventory>().bomb_count > 0) {
			StartCoroutine(bombAttack.Place());
		}

        if (Input.GetKeyDown(KeyCode.Z) && curWep == 2 && boomerangAttack.canThrow  && !inCustom && GameController.instance.canMove){
			StartCoroutine(boomerangAttack.Throw());        
		}

        if (Input.GetKeyDown(KeyCode.Z) && curWep == 3 && portalAttack.canFire && GameController.instance.canMove && inCustom){
			StartCoroutine(portalAttack.Fire());        
		}
    }

    public IEnumerator blink(GameObject blinkObj){
        while(true){
            if(blinkObj == null){
                break;
            }
			blinkObj.GetComponent<Renderer>().enabled = false;
			yield return new WaitForSeconds(.05f);
            if(blinkObj == null){
                break;
            }
			blinkObj.GetComponent<Renderer>().enabled = true;
			yield return new WaitForSeconds(.05f);
		}
    }

    IEnumerator goToCustom(){
        GameController.instance.clearActives();
        GameController.instance.player.GetComponent<Inventory>().hasPortalGun = GameController.instance.player.GetComponent<Inventory>().hasPortalGunPrev;
        StartCoroutine(GameController.instance.player.GetComponent<RoomMovement>().fadeScreen());
        GameController.instance.curRoom = new Vector2(20, 20);
        GameController.instance.player.transform.position = new Vector3(83f, 3f, 0f);
        yield return new WaitForSeconds(0.35f);
        GameController.instance.mainCamera.transform.position = new Vector3(87.5f, 7.5f, -10f);
        GameController.instance.player.GetComponent<Animator>().SetFloat("horizontal_input", 1);
    }

    IEnumerator goToNormal(){
        GameController.instance.clearActives();
        curWep = -1;
        leftUI.sprite = GameController.instance.black;
        GameController.instance.player.GetComponent<Inventory>().hasPortalGun = false;
        StartCoroutine(GameController.instance.player.GetComponent<RoomMovement>().fadeScreen());
        GameController.instance.curRoom = new Vector2(0, 0);
        GameController.instance.player.transform.position = new Vector3(39.5f, 1f, 0f);
        yield return new WaitForSeconds(0.35f);
        GameController.instance.mainCamera.transform.position = new Vector3(39.5f, 7.5f, -10f);
        GameController.instance.player.GetComponent<Animator>().SetFloat("vertical_input", 1);
    }
}
