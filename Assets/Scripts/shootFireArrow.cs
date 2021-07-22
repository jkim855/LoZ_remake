using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootFireArrow : MonoBehaviour
{
    public GameObject arrowPrefab;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        while (true)
        {
            float randoSound = Random.Range(0f, 1f);
            if (randoSound < 0.5)
            {
                AudioSource.PlayClipAtPoint(GameController.instance.arrowOneSound, Camera.main.transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(GameController.instance.arrowTwoSound, Camera.main.transform.position);
            }
            GameObject newArrow = Instantiate(arrowPrefab, new Vector3(transform.position.x + 0.2f, transform.position.y - 0.2f, transform.position.z), Quaternion.identity) as GameObject;
            while(newArrow != null)
            {
                yield return null;
            }
        }
    }
}