using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedDestruction : MonoBehaviour
{
    public float liveTime = 0.3f;
    float startTime;
    Death thisDeath;
    // Start is called before the first frame update
    void Start()
    {
        thisDeath = GetComponent<Death>();
        startTime = Time.time;
    }

    void Update(){
        if(Time.time - startTime >= liveTime){
            thisDeath.beginDeath();
        }
    }
}
