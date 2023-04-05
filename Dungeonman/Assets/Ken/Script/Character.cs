using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character: MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    protected float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Child class lis‰‰ oman logikaan miten ottaa damagee
    protected abstract void TakeDamage(float damage);

    // Child class lis‰‰ oman logikaan miten kuolee
    protected abstract void Death();
}
