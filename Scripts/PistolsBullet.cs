using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PistolsBullet : MonoBehaviour
{
    public float speed = 3f;
    public float StartPositionX = 0.8292f;
    public float StartPositionY = -0.2032f;
    public bool onFly = false;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private PolygonCollider2D coll;
    private MainCharacter mc;
    private Animator anim;


private void Start()
    {
        coll = GetComponent<PolygonCollider2D>();
        mc = GetComponentInParent<MainCharacter>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onFly)
        {
            if (collision.CompareTag("Enemy")){
                EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
                enemy.TakeDamage(60);
            }
            if (collision.CompareTag("Swordsman")){
                SwordsmanMovement enemy = collision.GetComponent<SwordsmanMovement>();
                enemy.TakeDamage(60);
            }
            if (collision.CompareTag("Boss"))
            {
                BossMovement enemy = collision.GetComponent<BossMovement>();
                enemy.TakeDamage(60);
            }
            if (collision.CompareTag("fireball"))
            {
                FireBall fireball = collision.GetComponent<FireBall>();
                fireball.HitTarget();
            }
        }
        if (!collision.CompareTag("Hero") && !collision.CompareTag("bullet") && !collision.CompareTag("dagger") && !collision.CompareTag("Sensor"))
        {
            HitTarget();
        }
    }
    public void HitTarget()
    {
        Destroy(rb);
        coll.enabled = false;
        onFly = false;
        sr.enabled = false;
        anim.SetBool("onFly", false);
        transform.position = mc.transform.position + new Vector3(mc.isRight ? StartPositionX : -StartPositionX, StartPositionY, 0);
        transform.rotation = Quaternion.Euler(0, mc.isRight ? 0 : 180, 0);
    }
public void Shot()
    {
        onFly = true;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        coll.enabled = true;
        sr.enabled = true;
        anim.SetBool("onFly", true);
        rb.velocity = new Vector2(mc.isRight ? speed : -speed, rb.velocity.y);
    }

}

