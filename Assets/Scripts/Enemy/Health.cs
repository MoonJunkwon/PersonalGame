using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;

    private int currentHealth;
    Knockback knockback;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
    }


    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockBack(PlayerControll.Instance.transform, 15f);
        Death();
    }

    private void Death()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
