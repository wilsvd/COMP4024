using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    int health = 200;
    private Animator bossAnimator;
    private BossWeapon sword;

    // Start is called before the first frame update
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
        sword = transform.Find("BossWeapon").transform.GetChild(0).GetComponent<BossWeapon>();
    }

    public void PerformAttack()
    {
        // Your attack logic here
        sword.Attack();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Console.WriteLine("Damage: " + health);
        // add check 
    }


    public bool isFlipped = false;

    public void LookAtPlayer(Transform player)
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            Flip(flipped);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            Flip(flipped);
            isFlipped = true;
        }
    }

    private void Flip(Vector3  flipped)
    {
        transform.localScale = flipped;
        transform.Rotate(0f, 180f, 0f);
    }
}
