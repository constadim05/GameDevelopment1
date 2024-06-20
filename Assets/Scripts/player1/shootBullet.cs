using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class shootBullet : MonoBehaviour, IPunObservable
{
    public float range = 10f;
    public float damage = 5f;
    public float fireRate = 0.5f; // Adjust this to control the rate of fire

    private float nextFireTime;
    private int shootableMask;
    private LineRenderer gunLine;
    private PhotonView photonView;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if (Time.time >= nextFireTime)
        {
            // Check for shooting input (if applicable)
            // Example: if (Input.GetButtonDown("Fire1"))
            Shoot();
        }
    }

    void Shoot()
    {
        photonView.RPC("RPC_Shoot", RpcTarget.All);
    }

    [PunRPC]
    void RPC_Shoot()
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

        // Enable the gunLine to make it visible and then disable it after a short delay
        StartCoroutine(ShowGunLine());
    }

    IEnumerator ShowGunLine()
    {
        gunLine.enabled = true;
        yield return new WaitForSeconds(0.1f); // Show the line for a short time
        gunLine.enabled = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send whether the gunLine is enabled or not
            stream.SendNext(gunLine.enabled);
        }
        else
        {
            // Receive whether the gunLine is enabled or not
            gunLine.enabled = (bool)stream.ReceiveNext();
        }
    }
}
