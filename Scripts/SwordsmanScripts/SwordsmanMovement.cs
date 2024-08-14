using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SwordsmanMovement : MonoBehaviour
{
    public MainCharacter target;

    public float Health = 250;
    public float moveSpeed = 3.0f;
    public float followDist = 12.0f;
    public int damage = 5;
    public float attackDistance = 1f;
    public float attackCooldown = 2.0f;
    public bool onAttack = false;
    public bool onLightAttack = false;
    public bool onHeavyAttack = false;

    private SpriteRenderer sr;
    private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;

    public bool isFollowing = false;
    public bool isAttacking = false;
    public bool freeze = false;

    private float nextRotate = 0f;
    private float rotateTime = 3f;
    private float lastAttackTime = 0f;

    private float lastAttackingTime = 0f;
    private float lastAttackingTimeCooldown = 0.5f;
    private float lastTakeDamageTime = 0f;
    private float lastTakeDamageTimeCooldown = 0.2f;
    private SwordsmanSensor sensor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sensor = GetComponentInChildren<SwordsmanSensor>();

    }

    private void Update()
    {

          if (target != null)
          {
                if(Health <= 0)
                {
                    Die();
                }
                if (onLightAttack && target.LightAttack)
                {
                    TakeDamage(10);
                }
                if (onHeavyAttack && target.HeavyAttack)
                {
                    TakeDamage(30);
                }

                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                if (distanceToTarget <= attackDistance && Time.time - lastAttackTime >= attackCooldown)
                {
                    freeze = true;
                    isAttacking = true;
                    anim.SetBool("Run", false);
                    anim.SetTrigger("Attack");
                    lastAttackTime = Time.time;
                }

                if (!freeze) {

                    if (distanceToTarget > followDist)
                    {
                        isFollowing = false;
                    }
                    else
                    {
                        isFollowing = true;
                    }


                    if (isFollowing && !isAttacking)
                    {
                        anim.SetBool("Run", true);
                        Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, 0);
                        transform.Translate(direction * moveSpeed * Time.deltaTime);

                        if (direction.x > 0)
                        {
                            sr.flipX = false;
                            sensor.transform.rotation = Quaternion.Euler(0, 0, 0);

                            
                        }
                        else
                        if (direction.x < 0)
                        {
                            sr.flipX = true;                          
                            sensor.transform.rotation = Quaternion.Euler(0, 180, 0);

                        }
                    }

                    if (!isFollowing && !isAttacking)
                    {
                        if (Time.time > nextRotate)
                        {
                            nextRotate = Time.time + rotateTime;
                            sr.flipX = !sr.flipX;
                        }
                        Vector3 newPosition = transform.position + (sr.flipX ? Vector3.left : Vector3.right) * 2 * Time.deltaTime;
                        sensor.transform.rotation = Quaternion.Euler(0, sr.flipX? 180 : 0, 0);
                        transform.position = newPosition;
                        anim.SetBool("Run", true);
                    }
            
                }
          }
    }
    public void TakeDamage(int damage)
    {
        if (Time.time - lastTakeDamageTime >= lastTakeDamageTimeCooldown)
        {
            Health -= damage;
            lastTakeDamageTime = Time.time;
        }
    }
    public void Unfreeze()
    {
        freeze = false;
    }
    public void Attacking()
    {
        if (onAttack && Time.time - lastAttackingTime >= lastAttackingTimeCooldown)
        {
            target.TakeDamage(damage);
            lastAttackingTime = Time.time;
        }
        isAttacking = false;
    }
    public void Die()
    {
        freeze = true;
        Destroy(rb);
        Destroy(coll);
        anim.SetBool("Run", false);
        DestroySelf();
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
