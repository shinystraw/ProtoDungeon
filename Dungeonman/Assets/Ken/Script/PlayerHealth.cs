using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Character
{
    [SerializeField] HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxhealth(maxHealth);
    }

    protected override void Death()
    {
        //animations
        Destroy(gameObject);
    }

    protected override void TakeDamage(float damage)
    {
        currentHealth += damage;
        healthBar.SetHealth(damage);

        if(currentHealth <= 0)
        {
            Death();
        }
    }
}
