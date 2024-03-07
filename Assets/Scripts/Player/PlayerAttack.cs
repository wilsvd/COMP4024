using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The PlayerAttack class manages the player's attacks, handling both melee and ranged attacks.
public class PlayerAttack : MonoBehaviour
{

    public Animator animSword;
    public Animator animBow;
    [SerializeField] private int meleeSpeed;
    [SerializeField] private int rangeSpeed;
    [SerializeField] private int damage;

    internal PlayerInventory inventory;

    float timeUntilAttack = 0;
    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the time until the next attack is zero and the space key is pressed.
        if (timeUntilAttack <= 0f && Input.GetKeyDown(KeyCode.Space))
        {
            // Check the equipped item type and perform the corresponding attack.
            if (inventory.equippedItem.type == PlayerInventory.Weapon.Sword)
            {
                SwordAttack();

            }
            else if (inventory.equippedItem.type == PlayerInventory.Weapon.Bow)
            {
                BowAttack();
            }

        }
        else
        {
            // Decrease the time until the next attack.
            timeUntilAttack -= Time.deltaTime;
        }
    }

    // Perform a sword attack.
    private void SwordAttack()
    {
        // Trigger the sword attack animation.
        inventory.equippedItem.item.transform.GetChild(0).GetComponent<Sword>().Attack();
        timeUntilAttack = meleeSpeed;
    }

    // Perform a bow attack.
    private void BowAttack()
    {
        // Trigger the bow attack animation and provide the player's facing direction.
        inventory.equippedItem.item.GetComponent<Bow>().Attack(transform.localScale.x);
        timeUntilAttack = rangeSpeed;
    }
}
