using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSensor : MonoBehaviour
{
    [SerializeField] BossMovement boss;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            boss.onAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            boss.onAttack = false;
        }
    }
}
