using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Image HPFilling;

    [SerializeField] MainCharacter health;

    private void Awake()
    {
         health.HealthChanged += onHealthChanged;
    }
    private void OnDestroy()
    {
        health.HealthChanged -= onHealthChanged;
    }

    private void onHealthChanged(float valueAsPercantage)
    {
       Debug.Log(valueAsPercantage);
       HPFilling.fillAmount = valueAsPercantage;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
