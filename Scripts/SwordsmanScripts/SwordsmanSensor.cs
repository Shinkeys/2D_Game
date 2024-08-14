using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanSensor : MonoBehaviour
{
    [SerializeField] SwordsmanMovement enemy;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            enemy.onAttack = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            enemy.onAttack = false;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
