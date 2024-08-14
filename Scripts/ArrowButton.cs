using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public ArrowShooter arrowShooter;
    public Sprite buttonPressed;
    private BoxCollider2D coll;
    private Animator animator;
    private SpriteRenderer sr;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ArrowButton Pressed");
        animator.SetBool("isPressed", true);
        arrowShooter.Shoot();
        Destroy(coll);
    }
    public void NotPressed()
    {
        Destroy(animator);
        sr.sprite = buttonPressed;
    }
}
