using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullet : MonoBehaviour
{

    public float range = 10f;
    public float damage = 5f;

    Ray shootRay;
    RaycastHit shootHit;

    public LayerMask shootableMask;

    LineRenderer gunLine;

    public static object Raycast { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        //shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        gunLine.SetPosition(0, transform.position);
        //gunLine.enabled = false;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(); // Call the Shoot method when Fire1 button is pressed
        }
    }

     void Shoot()
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

//hit an enemy goes here

gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction* range);
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
    
    
  
        