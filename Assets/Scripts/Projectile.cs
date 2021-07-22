using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool foundTarget = false;
    public bool foundOwner = false;
    Death deathScript;
    // Start is called before the first frame update
    void Start()
    {
        deathScript = GetComponent<Death>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(foundOwner){
            deathScript.beginDeath();
        }
        else if(foundTarget){
            if(this.gameObject.tag != "boomerangProjectile"){
                deathScript.beginDeath();
            }
        }
    }
}
