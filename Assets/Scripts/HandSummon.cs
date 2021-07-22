using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSummon : MonoBehaviour
{
    public int pos;
    public GameObject handObj;

    private GameObject hand;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameController.instance.curHandWait += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameController.instance.curHandWait < GameController.instance.handWait)
        {
            return;
        }
        GameController.instance.curHandWait = 0.0f;
        if (GameController.instance.activeHands.Contains(pos))
        {
            return;
        }
        GameController.instance.activeHands.Add(pos);
        if (pos == 0)
        {
           hand = Instantiate(handObj, new Vector3(68, 44, 0), Quaternion.identity) as GameObject;
        }
        if (pos == 1)
        {
            hand = Instantiate(handObj, new Vector3(75, 44, 0), Quaternion.identity) as GameObject;
        }
        if (pos == 2)
        {
            hand = Instantiate(handObj, new Vector3(80, 40, 0), Quaternion.identity) as GameObject;
        }
        if (pos == 3)
        {
            hand = Instantiate(handObj, new Vector3(80, 36, 0), Quaternion.identity) as GameObject;
        }
        if (pos == 4)
        {
            hand = Instantiate(handObj, new Vector3(75, 32, 0), Quaternion.identity) as GameObject;
        }
        if (pos == 5)
        {
            hand = Instantiate(handObj, new Vector3(68, 32, 0), Quaternion.identity) as GameObject;
        }
        if (pos == 6)
        {
            hand = Instantiate(handObj, new Vector3(63, 36, 0), Quaternion.identity) as GameObject;
        }
        if(pos == 7)
        {
            hand = Instantiate(handObj, new Vector3(63, 40, 0), Quaternion.identity) as GameObject;
        }
        hand.GetComponent<HandMovement>().pos = pos;
    }
}
