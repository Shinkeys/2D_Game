using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeIcon : MonoBehaviour
{

    public MainCharacter hero;
    
    public Sprite pistolsIcon;

    public Sprite greatswordIcon;

    private SpriteRenderer sr;
    void Start()
    {
       sr = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(hero.onPistols){
           sr.sprite = pistolsIcon;
        }
        else sr.sprite = greatswordIcon;
    }
    
}
