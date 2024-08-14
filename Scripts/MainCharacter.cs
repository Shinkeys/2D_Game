using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;
    public int maxHP = 100;
    public int currentHP = 0;
    private float currentHealthAsPercentage;
    public event Action<float> HealthChanged;
    public float damage = 25;

    public PistolsBullet bullet;
    public PistolsBullet bullet2;
    public MCAttackSensor heavy;
    public MCAttackSensor combo2;
    public MCAttackSensor combo3;

    public float attackTime = 1f;
    public float hardAttackTime = 2f;
    public float pistolShotTime = 4f;
    public float timeBetweenCombo = 0.4f;
    public float dashDistance = 5f;
    public float dashTime = 2f;

    public bool freeze = false;
    public bool isRight = true;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Dagger dagger;
    private DashSensor ds;

    private int PistolsState = 0;
    private int ComboState = 2;

    private float lastAttackTime = 0f;
    private float lastHardAttackTime = 0f;
    private float lastPistolShotTime = 0f;
    private float timeFromLastCombo = 0f;
    private float lastDashTime = 0f;

    public bool onPistols = false;
    private bool onAction = false;
    
    public bool onHeavyAttack = false;
    public bool HeavyAttack = false;
    public bool onLightAttack = false;
    public bool LightAttack = false;

    private bool SpacePress = false;
    private bool LMBPress = false;
    private bool RMBPress = false;
    private bool QPress = false;
    private bool XPress = false;
    private bool EPress = false;
    private bool CtrlPress = false;
    private bool TPress = false;

    public Teleport teleport;
    public Teleport teleport2;
    private bool teleporting = false;
    private float teleportStartTime = 0f;

    void Start()
    {
        timeFromLastCombo = Time.time;
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dagger = GetComponentInChildren<Dagger>();
        ds = GetComponentInChildren<DashSensor>();
    }

    // Update is called once per frame
    void Update()
    {
    // if (!animator.GetCurrentAnimatorStateInfo(0).IsName("pistols-shot")) {
        

        if (currentHP <= 0)
    {
        Death();
    }
        if (Time.time - timeFromLastCombo > 1f)
        {
            ComboState = 2;
        }

        if (Input.GetKeyDown(KeyCode.T))
            TPress = true;
        else
            TPress = false;
        if (Input.GetKeyDown(KeyCode.Space))
            SpacePress = true;
        else
            SpacePress = false;
        if (Input.GetKeyDown(KeyCode.LeftControl))
            CtrlPress = true;
        else
            CtrlPress = false;
        if (Input.GetKeyDown(KeyCode.Q))
            QPress = true;
        else
            QPress = false;
        if (Input.GetKeyDown(KeyCode.X))
            XPress = true;
        else
            XPress = false;
        if (Input.GetKeyDown(KeyCode.E))
            EPress = true;
        else
            EPress = false;
        if (Input.GetMouseButtonDown(0))
            LMBPress = true;
        else
            LMBPress = false;
        if (Input.GetMouseButtonDown(1))
            RMBPress = true;
        else
            RMBPress = false;

        if (SpacePress && Mathf.Abs(rb.velocity.y) < 0.05f && !onAction)
        {
            animator.SetBool("isJump", true);
            Jump();
        }

        if (CtrlPress && !onAction)
        {
            Debug.Log("Left CTRL pressed");
            Dash();
        }
        if (TPress && !teleporting)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("TPDoor") && collider.gameObject.layer == LayerMask.NameToLayer("TPDoor"))
                {
                    StartCoroutine(TeleportAnimation());
                    break;
                }
                else if (collider.CompareTag("TeleportToBoss") && collider.gameObject.layer == LayerMask.NameToLayer("TeleportToBoss")){
                    StartCoroutine(TeleportToBoss());
                    break; 
                }
                else if (collider.CompareTag("DoorToBoss") && collider.gameObject.layer == LayerMask.NameToLayer("DoorToBoss")){
                    StartCoroutine(DoorToBoss());
                    break; 
                }
                
            }
        }
        
        // Movement -- Start
        float inputX = Input.GetAxis("Horizontal");

        if (inputX == 0)
        {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalk", false);
        }
        else
        if (inputX > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 5f;
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalk", false);
            }
            else
            {
                speed = 3f;
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalk", true);
            }
            sr.flipX = false;
            ds.transform.rotation = Quaternion.Euler(0, 0, 0);
            isRight = true;
            if (!dagger.onFly)
            {
                dagger.transform.position = transform.position + new Vector3(0.69f, -0.43f, 0);
                dagger.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if(!bullet.onFly){
                bullet.transform.position = transform.position + new Vector3(0.8292f, -0.2032f, 0);
                bullet.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if(!bullet2.onFly){
                bullet2.transform.position = transform.position + new Vector3(0.8094f, -0.1424f, 0);
                bullet2.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (!HeavyAttack)
            {
                heavy.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (!LightAttack)
            {
                combo2.transform.rotation = Quaternion.Euler(0, 0, 0);
                combo3.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }
        else
        if (inputX < 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 5f;
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalk", false);
            }
            else
            {
                speed = 3f;
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalk", true);
            }
            sr.flipX = true;
            ds.transform.rotation = Quaternion.Euler(0, 180, 0);
            isRight = false;
            if (!dagger.onFly)
            {
                dagger.transform.position = transform.position + new Vector3(-0.69f, -0.43f, 0);
                dagger.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if(!bullet.onFly){
                bullet.transform.position = transform.position + new Vector3(-0.8292f, -0.2032f, 0);
                bullet.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if(!bullet2.onFly){
                bullet2.transform.position = transform.position + new Vector3(-0.8094f, -0.1424f, 0);
                bullet2.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (!HeavyAttack)
            {
                heavy.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (!LightAttack)
            {
                combo2.transform.rotation = Quaternion.Euler(0, 180, 0);
                combo3.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        if(!freeze){
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
        }
        // Movement -- End

        if (LMBPress && !onAction)
        {
            animator.SetBool($"Combo{ComboState}", true);
            timeFromLastCombo = Time.time;
            onAction = true;
        }

        if (QPress && Time.time - lastAttackTime >= attackTime && !onAction && !dagger.onFly)
        {
            freeze = true;
            animator.SetTrigger("isAttack");
            lastAttackTime = Time.time;
        }

        if (EPress && Time.time - lastHardAttackTime >= hardAttackTime && !onAction)
        {
            animator.SetTrigger("isHardAttack");
            lastHardAttackTime = Time.time;
        }

        if (XPress && Time.time - lastPistolShotTime >= pistolShotTime && !onAction && PistolsState == 0 && !bullet.onFly && !bullet2.onFly) {
            onAction = true;
            onPistols = true;
            animator.SetTrigger("GetPistols");
            lastPistolShotTime = Time.time;    
            speed = 0f;
            freeze = true;
            PistolsState = 1;
        }

        if (XPress && Time.time - lastPistolShotTime >= pistolShotTime && !onAction && PistolsState == 2 && !bullet2.onFly){
            onAction = true;
            onPistols = true;
            freeze = true;
            animator.SetTrigger("Get-1-pistol");
        }

        if (RMBPress && PistolsState == 1 && onPistols)
        {
            animator.SetTrigger("Drop-2-pistols");
            UnFreeze();
            PistolsState = 0;
            onAction = false;
            onPistols = false;
        } 
        else
        if (RMBPress && PistolsState == 2 && onPistols)
        {
            animator.SetTrigger("Drop-1-pistol");
            UnFreeze();
            onAction = false;
            onPistols = false;
        }

        if (LMBPress && PistolsState == 1 && onPistols)
        {
            animator.SetTrigger("Shot-2-pistols");
            PistolsState = 2;
            onAction = true;
            onPistols = true;
        }
        else
        if (LMBPress && PistolsState == 2 && onPistols)
        {
            animator.SetTrigger("Shot-1-pistol");
            PistolsState = 0;
            UnFreeze();
            onAction = false;
            onPistols = false;
        }
            
       
    }
    public void Attack(EnemyMovement enemy, int damage, bool condition)
    {
        if (condition)
        {
            enemy.TakeDamage(damage);
        }
    }
    public void HeavyAttacking(int state)
    {
        if (state == 1)
            HeavyAttack = true;
        if (state == 2)
            HeavyAttack = false;
    }
    public void LightAttacking(int state)
    {
        if (state == 1)
            LightAttack = true;
        if (state == 2)
            LightAttack = false;
    }
    public void Dash()
    {
        Vector2 dashDirection = new Vector2(isRight ? dashDistance : -dashDistance, 0).normalized;
        Vector2 dashPosition = rb.position + dashDirection * dashDistance;

        RaycastHit2D hit = Physics2D.Raycast(rb.position, dashDirection, dashDistance);

        if (!hit.collider.CompareTag("Ground") && Time.time - lastDashTime >= dashTime) {
            rb.MovePosition(dashPosition);
            lastDashTime = Time.time;
        }


    }
    public void TakeDamage(int damage)
{
    currentHP -= damage;
    if (currentHP <= 0)
    {
        Death();
    }
    else 
    {
        float currentHPAsPercentage = (float)currentHP / maxHP;
        currentHealthAsPercentage = currentHPAsPercentage;
        HealthChanged?.Invoke(currentHealthAsPercentage);
    }
}

     public void RestoreHealth(float amount)
{
    Debug.Log("Restoring health: " + amount);

    currentHP = Mathf.Clamp(currentHP + (int)amount, 0, maxHP);

    float currentHPAsPercentage = (float)currentHP / maxHP;
    currentHealthAsPercentage = currentHPAsPercentage;

    HealthChanged?.Invoke(currentHealthAsPercentage);
}
    public void Death(){
        
        string sceneToLoad = "Exit";
        SceneManager.LoadScene(sceneToLoad);
    }
    public void ContinueCombo()
    {
        animator.SetBool($"Combo{ComboState}", false);
        if (ComboState < 3)
            ComboState++;
        else
            ComboState = 2;

        onAction = false;
    }
    public void UnFreeze(){
        freeze = false;
    }
    public void DaggerAttack()
    {
        dagger.ThrowDagger();
    }
    public void PistolsShot(){
        if (!bullet.onFly){
            bullet.Shot();
        }  
    }
    public void PistolsShot2(){
        if (!bullet2.onFly)
            bullet2.Shot();
    }
    public void Jump()
    {
        animator.SetBool("isJump", false);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

 IEnumerator TeleportAnimation()
    {
        teleporting = true;
        teleportStartTime = Time.time;
        teleport.PlayTeleportAnimation();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("level 2");
    }
    IEnumerator TeleportToBoss()
    {
        teleporting = true;
        teleportStartTime = Time.time;
        teleport.PlayTeleportAnimation();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("level 3");
    }
    IEnumerator DoorToBoss()
    {
        teleporting = true;
        teleportStartTime = Time.time;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("level 3");
    }
    
}
