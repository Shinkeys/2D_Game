using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    [SerializeField] public Arrow arrow;
    public bool isLeft = true;
    public Sprite DefaultShooter;

    private SpriteRenderer sr;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        arrow = GetComponentInChildren<Arrow>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.flipX)
        {
            isLeft = false;
        }
        else
        {
            isLeft = true;
        }
    }
    public void Shoot()
    {
        Debug.Log("ArrowShooter Shoot");
        arrow.enabled = true;
        animator.SetBool("isShooting", true);
        arrow.Fly();
    }
    public void EndAnim()
    {
        sr.sprite = DefaultShooter;
        Destroy(animator);
    }
}
