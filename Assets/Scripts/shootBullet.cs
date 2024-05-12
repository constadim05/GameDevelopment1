using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class shootBullet : MonoBehaviour
{
    public float range = 10f;
    public float damage = 5f;
    public float fireRate = 0.5f; // Adjust this to control the rate of fire

    float nextFireTime;
    int shootableMask;
    LineRenderer gunLine;

    PlayerControls controls;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();

        // Initialize input controls
        controls = new PlayerControls();
        controls.Player.Shoot.performed += _ => Shoot(); // Call Shoot() when the shoot action is performed
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Shoot()
    {
        if (Time.time >= nextFireTime)
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
}
