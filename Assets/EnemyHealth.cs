using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    public float damageModifier;
    public bool drops;
    public GameObject drop1; // First drop variable
    public GameObject drop2; // Second drop variable
    public AudioClip deathSound;
    public bool canBurn;
    public float burnDamage;
    public float burnTime;
    public GameObject burnEffects;

    bool onFire;
    float nextBurn;
    float burnInterval = 1f;
    float endBurn;

    float currentHealth;

    public Slider enemyHealthIndicator;

    AudioSource enemyAS;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyMaxHealth;
        enemyHealthIndicator.maxValue = enemyMaxHealth;
        enemyHealthIndicator.value = currentHealth;
        enemyAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onFire && Time.time > nextBurn)
        {
            addDamage(burnDamage);
            nextBurn += burnInterval;
        }
        if (onFire && Time.time > endBurn)
        {
            onFire = false;
            burnEffects.SetActive(false);
        }
    }

    public void addDamage(float damage)
    {
        enemyHealthIndicator.gameObject.SetActive(true);
        damage = damage * damageModifier;
        if (damage <= 0f) return;
        currentHealth -= damage;
        enemyHealthIndicator.value = currentHealth;
        enemyAS.Play();
        if (currentHealth <= 0) makeDead();
    }

    public void addFire()
    {
        if (!canBurn) return;
        onFire = true;
        burnEffects.SetActive(true);
        endBurn = Time.time + burnTime;
        nextBurn = Time.time + burnInterval;
    }

    void makeDead()
    {
        if (gameObject == null || gameObject.transform.root == null) return; // Check if GameObject or its root is already destroyed

        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.15f);

        // Deactivate the health indicator if it exists
        if (enemyHealthIndicator != null)
        {
            enemyHealthIndicator.gameObject.SetActive(false);
        }

        if (drops)
        {
            GameObject dropToInstantiate = Random.Range(0, 2) == 0 ? drop1 : drop2; // Randomly choose between drop1 and drop2
            Vector3 spawnPosition = transform.position + Vector3.up * 0.5f;
            Instantiate(dropToInstantiate, spawnPosition, Quaternion.identity);
        }

        Destroy(gameObject.transform.root.gameObject);
    }


}