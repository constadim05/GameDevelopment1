using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickupController : MonoBehaviour
{
    public int ammoAmount;
    public AudioClip ammoPickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fireBullet fireBulletComponent = other.GetComponentInChildren<fireBullet>();

            if (fireBulletComponent != null)
            {
                fireBulletComponent.addAmmo(ammoAmount);
                AudioSource.PlayClipAtPoint(ammoPickupSound, transform.position, 0.15f);
                Destroy(transform.root.gameObject);
            }
        }
    }
}
