using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerangControl : MonoBehaviour
{
    bool outward = true;    
    float projectileSpeed = 10.0f;
    float airTime = 0.2f;
    public GameObject owner;
    // Start is called before the first frame update
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(owner.tag == "Player"){
            GetComponent<AudioSource>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(outward){
            rb.transform.Rotate(0.0f, 0.0f, 20.0f);
        }
        else{
            rb.transform.Rotate(0.0f, 0.0f, -20.0f);
        }
        if(owner == null){
            Destroy(this.gameObject);
        }
    }

    public IEnumerator boomerangMovement(Vector3 directionVector){
        for (float t = 0; t < airTime; t = Mathf.Min(t + Time.deltaTime, airTime)) {
            if(this == null || GetComponent<Projectile>().foundTarget){
                break;
            }
            GetComponent<Rigidbody>().velocity = directionVector * projectileSpeed * (1 - (t/airTime));
            yield return new WaitForSeconds(.05f);
        }
        if(this != null){
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            outward = false;
        }
        while(true){
            if(this == null){
                break;
            }
            GetComponent<Rigidbody>().velocity = (owner.transform.position - this.transform.position).normalized * projectileSpeed;
            yield return new WaitForSeconds(.1f);
        }
    }
}
