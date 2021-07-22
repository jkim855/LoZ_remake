using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currHealth;

    Death deathHandler;

    // Start is called before the first frame update
    void Start()
    {
        deathHandler = GetComponent<Death>();
    }
    
    public void AddHealth(float health){
        if(health > 0){
            currHealth = Mathf.Min(currHealth + health, maxHealth);
        }
        else{
            currHealth = Mathf.Max(currHealth + health, 0);
        }

        if(currHealth == 0){
            deathHandler.beginDeath();
        }
    }
}
