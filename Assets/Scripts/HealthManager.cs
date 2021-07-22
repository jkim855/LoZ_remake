using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Health playerHealth;
    public Sprite fullHealth;
    public Sprite halfHealth;
    public Sprite noHealth;

    private Image[] Hearts = new Image[4];
    public Image firstHeart;
    public Image secondHeart;
    public Image thirdHeart;
    public Image fourthHeart;

    void Start(){
        Hearts[0] = firstHeart;
        Hearts[1] = secondHeart;
        Hearts[2] = thirdHeart;
        Hearts[3] = fourthHeart;
    }

    void Update(){
        for(int q=0; q<4; q++){
            if(Hearts[q] != null){
                Hearts[q].gameObject.SetActive(true);
                if((playerHealth.maxHealth) <= q){
                    Hearts[q].gameObject.SetActive(false);
                    continue;
                }
                if(playerHealth.currHealth - q >= 1){
                    Hearts[q].sprite = fullHealth;
                }
                else if(playerHealth.currHealth - q >= 0.5){
                    Hearts[q].sprite = halfHealth;
                }
                else{
                    Hearts[q].sprite = noHealth;
                }
            }
        }
    }
}
