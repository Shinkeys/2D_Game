using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    public void PlayTeleportAnimation()
    {
        StartCoroutine(TeleportAnimationCoroutine());
    }

    private IEnumerator TeleportAnimationCoroutine()
    {
        animator.SetTrigger("TPAnim");
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(2f); 

        
    }
}