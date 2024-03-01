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
            timeUntilAttack -= Time.deltaTime;
        }
    }

    private void SwordAttack()
    {
        inventory.equippedItem.transform.GetChild(0).GetComponent<Sword>().Attack();
        timeUntilAttack = meleeSpeed;
    }

    private void BowAttack()
    {
        inventory.equippedItem.GetComponent<Bow>().Attack(transform.localScale.x);
        timeUntilAttack = rangeSpeed;
    }
}
