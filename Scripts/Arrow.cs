using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{
    public float speed = 2f;
    private ArrowShooter shooter;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponentInParent<ArrowShooter>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fly()
    {
        Debug.Log("Arrow Fly");
        rb.velocity = new Vector2(shooter.isLeft ? -speed : speed, rb.velocity.y);
    }
}
