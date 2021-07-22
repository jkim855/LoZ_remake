using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    private Animator animator;
    private Coroutine attackMove;

    public GameObject bulletPrefab;
    public AnimationClip moveAnim;
    public AnimationClip attackAnim;
    public AudioClip attackAudio;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attackMove = StartCoroutine(makeAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator makeAttack()
    {
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        while (true)
        {
            yield return new WaitForSeconds(3);
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(moveAnim, attackAnim));
            aoc.ApplyOverrides(anims);
            animator.runtimeAnimatorController = aoc;
            GameObject cenBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            GameObject upBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            GameObject downBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            anims.Clear();
            AudioSource.PlayClipAtPoint(attackAudio, Camera.main.transform.position);
            yield return new WaitForSeconds(0.5f);
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(moveAnim, moveAnim));
            aoc.ApplyOverrides(anims);
            animator.runtimeAnimatorController = aoc;
            anims.Clear();
        }
    }
}
