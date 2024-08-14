using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public MainCharacter target;
    public Sprite death;

    public float Health = 500;
    public float moveSpeed = 2.0f;
    public float followDist = 6.0f;
    public int damage = 5;
    public float closeAttackDistance = 3f;
    public float fireBallAttackDistance = 10f;
    public float attackCooldown = 2.0f;
    public bool onAttack = false;
    public bool onLightAttack = false;
    public bool onHeavyAttack = false;

    private SpriteRenderer sr;
    private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private FireBall fireball;

    private bool isFollowing = false;
    private bool isAttacking = false;
    public bool freeze = false;
    public bool isRight = false;

    private float lastAttackTime = 0f;

    private float lastAttackingTime = 0f;
    private float lastAttackingTimeCooldown = 0.05f;
    private float lastTakeDamageTime = 0f;
    private float lastTakeDamageTimeCooldown = 0.2f;
    private bool isDead = false;
    private BossAttackSensor sensor;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sensor = GetComponentInChildren<BossAttackSensor>();
        fireball = GetComponentInChildren<FireBall>();
    }

    private void Update()
    {
        if (target != null && !isDead)
        {
            if (Health <= 0)
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

            if (distanceToTarget <= closeAttackDistance && Time.time - lastAttackTime >= attackCooldown)
            {
                freeze = true;
                isAttacking = true;
                anim.SetBool("Move", false);
                anim.SetTrigger("Attack2");
                lastAttackTime = Time.time;
            }
            if (distanceToTarget > closeAttackDistance && distanceToTarget <= fireBallAttackDistance && Time.time - lastAttackTime >= attackCooldown && !fireball.onFly)
            {
                freeze = true;
                isAttacking = true;
                anim.SetBool("Move", false);
                anim.SetTrigger("Attack1");
                lastAttackTime = Time.time;
            }
            if (!freeze)
            {

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
                    anim.SetBool("Move", true);
                    Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, 0);
                    transform.Translate(direction * moveSpeed * Time.deltaTime * 0.3f);

                    if (direction.x > 0)
                    {
                        sr.flipX = false;
                        isRight = true;
                        sensor.transform.rotation = Quaternion.Euler(0, 180, 0);
                        if (!fireball.onFly)
                        {
                            fireball.transform.position = transform.position + new Vector3(0.5f, 0.29f, 0);
                            fireball.transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                    }
                    else
                    if (direction.x < 0)
                    {
                        sr.flipX = true;
                        isRight = false;
                        sensor.transform.rotation = Quaternion.Euler(0, 0, 0);
                        fireball.transform.position = transform.position + new Vector3(-0.5f, 0.29f, 0);
                        fireball.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }

                if (!isFollowing && !isAttacking)
                {
                    anim.SetBool("Move", false);
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
            target.TakeDamage(1);
            lastAttackingTime = Time.time;
        }
        isAttacking = false;
    }
    public void ThrowFireBall()
    {
        fireball.Shot();
    }
    public void Die()
    {
        freeze = true;
        Destroy(rb);
        Destroy(coll);
        anim.SetBool("Move", false);
        anim.SetBool("Die", true);
    }
    public void DestroySelf()
    {
        isDead = true;
        Destroy(anim);
        sr.sprite = death;
        Destroy(gameObject, 10);
    }
}
