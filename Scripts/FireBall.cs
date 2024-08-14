using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
 
    public Transform target;

    public float speed = 0.5f;
    public float StartPositionX = 0;
    public float StartPositionY = 0;
    public bool onFly = false;
    public float rotationSpeed = 3f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private PolygonCollider2D coll;
    private BossMovement boss;
    private Animator anim;
    private Vector3 gotPosition;


    private void Start()
    {
        coll = GetComponent<PolygonCollider2D>();
        boss = GetComponentInParent<BossMovement>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onFly)
        {
            if (collision.CompareTag("Hero"))
            {
                MainCharacter mc = collision.GetComponent<MainCharacter>();
                mc.TakeDamage(10);
            }
            if (collision.CompareTag("Enemy"))
            {
                EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
                enemy.TakeDamage(10);
            }
            if (!collision.CompareTag("bullet") && !collision.CompareTag("dagger") && !collision.CompareTag("Sensor") && !collision.CompareTag("Boss"))
            {
                HitTarget();
            }
        }
    }
    public void HitTarget()
    {
        Destroy(rb);
        coll.enabled = false;
        onFly = false;
        sr.enabled = false;
        anim.SetBool("onFly", false);
        transform.position = boss.transform.position + new Vector3(boss.isRight ? 0.5f : -0.5f, 0.29f, 0);
        transform.rotation = Quaternion.Euler(0, boss.isRight ? 180 : 0, 0);
    }
    public void Shot()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        coll.enabled = true;
        sr.enabled = true;
        anim.SetBool("onFly", true);
        gotPosition = target.position;
        onFly = true;
        Vector2 direction = gotPosition - transform.position;
        rb.velocity = direction * speed;
    }
    private void Update()
    {
        if (onFly)
        {
            
        }
    }
}
