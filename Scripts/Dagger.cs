using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    [SerializeField] public Sprite sprite;
    public float speed = 3f;
    public bool onFly = false;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private PolygonCollider2D coll;
    private MainCharacter mc;
    private CircleCollider2D hitSensor;

    private void Start()
    {
        coll = GetComponent<PolygonCollider2D>();
        mc = GetComponentInParent<MainCharacter>();
        hitSensor = GetComponentInChildren<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onFly)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
                enemy.TakeDamage(40);
            }
            if (collision.CompareTag("Swordsman"))
            {
                SwordsmanMovement enemy = collision.GetComponent<SwordsmanMovement>();
                enemy.TakeDamage(40);
            }
            if (collision.CompareTag("Boss"))
            {
                BossMovement enemy = collision.GetComponent<BossMovement>();
                enemy.TakeDamage(40);
            }
            if (collision.CompareTag("fireball"))
            {
                FireBall fireball = collision.GetComponent<FireBall>();
                fireball.HitTarget();
            }
        }
        if (!collision.CompareTag("Hero") && !collision.CompareTag("bullet") && !collision.CompareTag("Sensor"))
        {
            HitTarget();
        }
    }
    public void HitTarget()
    {
        Destroy(rb);
        Destroy(sr);
        coll.enabled = false;
        onFly = false;
        transform.position = mc.transform.position + new Vector3(mc.isRight ? 0.69f : -0.69f, -0.43f, 0);
        transform.rotation = Quaternion.Euler(0, mc.isRight ? 0 : 180, 0);
    }
    public void ThrowDagger()
    {
        onFly = true;
        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        coll.enabled = true;
        rb.velocity = new Vector2(mc.isRight ? speed : -speed, rb.velocity.y);
    }
}
