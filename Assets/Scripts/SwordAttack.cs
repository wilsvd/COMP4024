using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    public Animator anim;
    [SerializeField] private int meleeSpeed;
    [SerializeField] private int damage;

    internal Inventory inventory;

    float timeUntilAttack = 0;

    // Update is called once per frame
    void Update()
    {
        
        if (timeUntilAttack <= 0f)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Attack");
                anim.SetTrigger("Attack");
                timeUntilAttack = meleeSpeed;
            }
        }
        else
        {
            Debug.Log(Time.deltaTime);
            timeUntilAttack -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().TakeDamage(damage);
            Debug.Log("Enemy hit");
        }*/
    }
}
