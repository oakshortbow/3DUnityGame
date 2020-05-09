﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    // Start is called before the first frame update
    public int maxHealth = 100;
    public int currHealth;

    private void Start()
    {
      currHealth = maxHealth;  
    }


    public void DecreaseCurrentHealth(int damage) {
        currHealth -= damage;
        if(currHealth < 0) {
            currHealth = 0;
        }
        //Post Death Event
    }

    public void Heal(int health) {
        int amountHealed = health + currHealth;
        if(amountHealed > maxHealth) {
            amountHealed = maxHealth;
        }
        currHealth = amountHealed;       
    }



}
