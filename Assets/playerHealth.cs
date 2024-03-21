using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public float fullHealth;
    float currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            makeDead();
        }
    }

    public void makeDead()
    {
        Destroy(gameObject);
    }
}
