using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{

    Sprite startSprite;
    public Sprite secondSprite;
    // Start is called before the first frame update
    void Start()
    {
        startSprite = GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(flicker());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator flicker(){
        int counter = 0;
        while(this != null){
            if(counter == 0){
                GetComponent<SpriteRenderer>().sprite = startSprite;
                counter = 1;
            }
            else{
                GetComponent<SpriteRenderer>().sprite = secondSprite;
                counter = 0;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
