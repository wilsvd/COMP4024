using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Animator animSword;
    public Animator animBow;
    [SerializeField] private int meleeSpeed;
    [SerializeField] private int rangeSpeed;
    [SerializeField] private int damage;

    internal Inventory inventory;

    float timeUntilAttack = 0;
    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timeUntilAttack <= 0f)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                /*
                 * TODO: Find a better method of checking item instead of hard coded name
                 */
                if (inventory.equippedItem.name == "Sword(Clone)")
                {
                    SwordAttack();

                }
                else if (inventory.equippedItem.name == "Bow(Clone)")
                {
                    BowAttack();
                }
            }
        }
        else
        {
            Debug.Log(Time.deltaTime);
            timeUntilAttack -= Time.deltaTime;
        }
    }

    private void SwordAttack()
    {
        
        inventory.equippedItem.GetComponent<Animator>().SetTrigger("SwordAttack");
        timeUntilAttack = meleeSpeed;
    }

    private void BowAttack()
    {
        /*
        * TODO: Implement a Bow projectile with animation
        */
        timeUntilAttack = rangeSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Boss"))
        {
            // Handle damage based on equipped item (sword or bow)
            if (false *//*Check if sword is equipped*//*)
                {
                    other.GetComponent<Boss>().TakeDamage(meleeDamage);
                }
                else if (false *//*Check if bow is equipped*//*)
                    {
                        other.GetComponent<Boss>().TakeDamage(rangedDamage);
                    }

            Debug.Log("Enemy hit");
        }*/
    }
}
