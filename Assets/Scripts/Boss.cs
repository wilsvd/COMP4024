using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    int health = 200;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Console.WriteLine("Damage: " + health);
        // add check 
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
