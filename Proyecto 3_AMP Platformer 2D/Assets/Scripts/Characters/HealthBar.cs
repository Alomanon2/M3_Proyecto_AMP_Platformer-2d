using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public GameObject boss;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("BOSS");
    }
    
    private void Update()
    {
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health; 
        slider.value = health;
        
    }    

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
