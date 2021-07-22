using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrowMove : MonoBehaviour
{
    public float flySpeed = 5f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = GetComponent<arrowDirection>().flyDir * flySpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!GameController.instance.god_mode)
            {
                collision.gameObject.GetComponent<Health>().AddHealth(-3);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("snorlax"))
        {
            Destroy(collision.gameObject);
            for(int q=0; q<GameController.instance.objectsToSpawn[GameController.instance.curRoom].Count; q++){
                if(GameController.instance.objectsToSpawn[GameController.instance.curRoom][q].tag == "snorlax"){
                    GameController.instance.objectsToSpawn[GameController.instance.curRoom].RemoveAt(q);
                    break;
                }
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Destroy(gameObject);
        }
        
    }
}
