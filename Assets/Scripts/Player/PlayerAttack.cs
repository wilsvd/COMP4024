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

        if (timeUntilAttack <= 0f && Input.GetKeyDown(KeyCode.Space))
        {

            if (inventory.equippedItem.type == Inventory.Weapon.Sword)
            {
                SwordAttack();

            }
            else if (inventory.equippedItem.type == Inventory.Weapon.Bow)
            {
                BowAttack();
            }

        }
        else
        {
            timeUntilAttack -= Time.deltaTime;
        }
    }

    private void SwordAttack()
    {
        inventory.equippedItem.item.transform.GetChild(0).GetComponent<Sword>().Attack();
        timeUntilAttack = meleeSpeed;
    }

    private void BowAttack()
    {
        inventory.equippedItem.item.GetComponent<Bow>().Attack(transform.localScale.x);
        timeUntilAttack = rangeSpeed;
    }
}
