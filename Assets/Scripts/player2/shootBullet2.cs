using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullet2 : MonoBehaviour
{
    public float range = 10f;
    public float damage = 5f;
    public float fireRate = 0.5f; // Adjust this to control the rate of fire

    float nextFireTime;
    int shootableMask;
    LineRenderer gunLine;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            // Check for shooting input (if applicable)
            // Example: if (Input.GetButtonDown("Fire1"))
            Shoot();
        }
    }

    void Shoot()
    {
        Ray shootRay = new Ray(transform.position, transform.forward);
        RaycastHit shootHit;

        gunLine.SetPosition(0, transform.position);

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            if (shootHit.collider.CompareTag("Enemy"))
            {
                EnemyHealth theEnemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                if (theEnemyHealth != null)
                {
                    theEnemyHealth.addDamage(damage);
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

        // Set the next fire time based on fire rate
        nextFireTime = Time.time + fireRate;
    }
}
