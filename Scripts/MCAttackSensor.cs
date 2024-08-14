using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MCAttackSensor : MonoBehaviour
{
    public int attackState = 0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (attackState == 1)
            {
                collision.GetComponent<EnemyMovement>().onLightAttack = true;
            }
            if (attackState == 2)
            {
                collision.GetComponent<EnemyMovement>().onHeavyAttack = true;
            }
        }
        if (collision.CompareTag("Swordsman"))
        {
            if (attackState == 1)
            {
                collision.GetComponent<SwordsmanMovement>().onLightAttack = true;
            }
            if (attackState == 2)
            {
                collision.GetComponent<SwordsmanMovement>().onHeavyAttack = true;
            }
        }
        if (collision.CompareTag("Boss"))
        {
            if (attackState == 1)
            {
                collision.GetComponent<BossMovement>().onLightAttack = true;
            }
            if (attackState == 2)
            {
                collision.GetComponent<BossMovement>().onHeavyAttack = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (attackState == 1)
            {
                collision.GetComponent<EnemyMovement>().onLightAttack = false;
            }
            if (attackState == 2)
            {
                collision.GetComponent<EnemyMovement>().onHeavyAttack = false;
            }
        }
        if (collision.CompareTag("Swordsman"))
        {
            if (attackState == 1)
            {
                collision.GetComponent<SwordsmanMovement>().onLightAttack = false;
            }
            if (attackState == 2)
            {
                collision.GetComponent<SwordsmanMovement>().onHeavyAttack = false;
            }
        }
        if (collision.CompareTag("Boss"))
        {
            if (attackState == 1)
            {
                collision.GetComponent<BossMovement>().onLightAttack = false;
            }
            if (attackState == 2)
            {
                collision.GetComponent<BossMovement>().onHeavyAttack = false;
            }
        }
    }
}
