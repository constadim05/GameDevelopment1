using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullet : MonoBehaviour
{
    public float range = 10f;
    public float damage = 5f;
<<<<<<< HEAD
    private Ray shootRay;
    private RaycastHit shootHit;
    public LayerMask shootableMask;
    private LineRenderer gunLine;
=======

    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
>>>>>>> parent of 714ff52 (2/5)

    // Start is called before the first frame update
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
<<<<<<< HEAD
<<<<<<< HEAD
        shootRay = new Ray(transform.position, transform.forward);
        gunLine.SetPosition(0, transform.position);
        gunLine.enabled = true; // Enable the gunLine by default
    }

    void Update()
    {
        RenderGunLine();

        if (Input.GetButtonDown("Fire1"))
        {
           // Debug.Log("Fire1 button pressed");

            Shoot();
        }
    }

    void Shoot()
    {
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //Debug.Log("Raycast hit something");
           // Debug.Log("Hit object name: " + shootHit.collider.gameObject.name);

            if (shootHit.collider.tag == "Enemy")
            {
               // Debug.Log("Hit an enemy");

                enemyHealth theEnemyHealth = shootHit.collider.GetComponent<enemyHealth>();
                theEnemyHealth.addDamage(damage);
                theEnemyHealth.damageFX(shootHit.point, -shootRay.direction);
            }
            
        }
    }

    void RenderGunLine()
    {
        if (Input.GetButton("Fire1"))
        {
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
        else
        {
            gunLine.SetPosition(0, transform.position);
            gunLine.SetPosition(1, transform.position);
        }
    }
}

/* void Shoot()
{
    {
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            Debug.Log(shootHit.collider.gameObject.name);
            if (shootHit.collider.tag == "Enemy")
            {
                enemyHealth theEnemyHealth = shootHit.collider.GetComponent<enemyHealth>();
                theEnemyHealth.addDamage(damage);
                theEnemyHealth.damageFX(shootHit.point, -shootRay.direction);
            }

=======

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        gunLine.SetPosition(0, transform.position);
        //gunLine.enabled = false;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))

        //hit an enemy goes here
        {
>>>>>>> parent of 714ff52 (2/5)
=======

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        gunLine.SetPosition(0, transform.position);
        //gunLine.enabled = false;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))

        //hit an enemy goes here
        {
>>>>>>> parent of 714ff52 (2/5)
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            Debug.Log("Raycast didn't hit anything"); // Add this line to check if the ray is not hitting anything
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
}
*/



