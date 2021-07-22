using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

	Renderer renderer;

	public float knockbackTime = 0.5f;
	public float knockbackSpeed = 5f;
	public bool invulnerable = false;
	public float invulnTime = 1f;
	bool stunned = false;
	float stunTime = 0.2f;
	float curStun = 0.0f;
	Health thisHealth;

	void Start() {
		renderer = this.GetComponent<Renderer>();
		thisHealth = this.GetComponent<Health>();
	}

	public void OnHit(Vector2 direction, float damage) {
		if (!stunned && invulnerable){
			return;
		}
		thisHealth.AddHealth(damage);
		if(this.gameObject.tag == "Player"){
			AudioSource.PlayClipAtPoint(GameController.instance.playerHurtAudio, Camera.main.transform.position);
		}

		if(thisHealth.currHealth > 0){
			StartCoroutine(Knockback(direction));
			StartCoroutine(Invincibility());
		}
	}

	public void OnStun(){
		if(invulnerable){
			return;
		}
		curStun = stunTime;
		invulnerable = true;
		AudioSource.PlayClipAtPoint(GameController.instance.stunAudio, Camera.main.transform.position);
		if(!stunned){
			stunned = true;
			StartCoroutine(stunThis());
		}
	}

	

	IEnumerator stunThis(){
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),this.gameObject.layer, true);
		if(this.tag == "skeleton"){
			this.GetComponent<SkeletonMovement>().canMove = false;
		}
		if(this.tag == "boomerang"){
			this.GetComponent<BoomerangMovement>().canMove = false;
		}
		if(this.tag == "hand"){
			this.GetComponent<HandMovement>().canMove = false;
		}
		if(this.tag == "dragon"){
			this.GetComponent<DragonMovement>().canMove = false;
		}

		this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
		while(curStun > 0){
			curStun -= Time.deltaTime;
			yield return new WaitForSeconds(0.1f);
		}

		if(this.tag == "skeleton"){
			this.GetComponent<SkeletonMovement>().canMove = true;
		}
		if(this.tag == "boomerang"){
			this.GetComponent<BoomerangMovement>().canMove = true;
		}
		if(this.tag == "hand"){
			this.GetComponent<HandMovement>().canMove = true;
		}
		if(this.tag == "dragon"){
			this.GetComponent<DragonMovement>().canMove = true;
		}

		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),this.gameObject.layer, false);
		stunned = false;
		invulnerable = false;
	}

	IEnumerator Knockback(Vector2 direction) {
		if(this.tag == "Player"){
			GameController.instance.canMove = false;
		}
		if(this.tag == "skeleton"){
			this.GetComponent<SkeletonMovement>().canMove = false;
		}
        if(this.tag == "hand")
        {
            this.GetComponent<HandMovement>().canMove = false;
        }
		if(this.tag == "boomerang"){
			this.GetComponent<BoomerangMovement>().canMove = false;
		}
        this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
		for (float t = 0; t < knockbackTime; t += Time.deltaTime) {
			this.GetComponent<Rigidbody>().velocity = (Vector3)direction * knockbackSpeed;
			yield return new WaitForFixedUpdate();
		}
		if(this.tag == "Player"){
			GameController.instance.canMove = true;
		}
		if(this.tag == "skeleton"){
			this.GetComponent<SkeletonMovement>().canMove = true;
		}
        if(this.tag == "hand")
        {
            this.GetComponent<HandMovement>().canMove = true;
        }
		if(this.tag == "boomerang"){
			this.GetComponent<BoomerangMovement>().canMove = true;
		}
    }


	IEnumerator Invincibility() {
		invulnerable = true;
		for (float endTime = Time.time + invulnTime; Time.time < endTime; ) {
			renderer.enabled = false;
			yield return new WaitForSeconds(.1f);
			renderer.enabled = true;
			yield return new WaitForSeconds(.1f);
		}
		invulnerable = false;
	}
}