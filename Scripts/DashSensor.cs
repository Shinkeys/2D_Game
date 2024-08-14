using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSensor : MonoBehaviour
{
    public MainCharacter character;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            character.dashDistance = MathF.Abs(character.transform.position.x - collision.ClosestPoint(transform.position).x);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        character.dashDistance = 5f;
    }
}
