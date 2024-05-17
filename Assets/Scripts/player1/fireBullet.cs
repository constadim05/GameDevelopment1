using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fireBullet : MonoBehaviour
{
    public float timeBetweenBullets = 0.15f;
    public GameObject projectile;

    public Slider playerAmmoSlider;
    public int maxRounds;
    public int startingRounds;
    int remainingRounds;

    float nextBullet;

    //audio info
    AudioSource gunMuzzleAS;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    void Awake()
    {
        nextBullet = 0f;
        remainingRounds = startingRounds;
        playerAmmoSlider.maxValue = maxRounds;
        playerAmmoSlider.value = remainingRounds;
        gunMuzzleAS = GetComponent<AudioSource>();
    }

    void Update()
    {
        playerController myPlayer = transform.root.GetComponent<playerController>();

        if (myPlayer == null)
        {
            Debug.LogError("playerController not found!");
            return;
        }

        if (myPlayer._playerID == 1 && Input.GetKeyDown(KeyCode.Space) && nextBullet < Time.time && remainingRounds > 0)
        {
            Fire(myPlayer);
        }

        if (myPlayer._playerID == 2 && Input.GetKeyDown(KeyCode.M) && nextBullet < Time.time && remainingRounds > 0)
        {
            Fire(myPlayer);
        }
    }

    void Fire(playerController myPlayer)
    {
        nextBullet = Time.time + timeBetweenBullets;
        Vector3 rot;
        if (myPlayer.GetFacing() == -1f)
        {
            rot = new Vector3(0, -90, 0);
        }
        else
        {
            rot = new Vector3(0, 90, 0);
        }

        // Instantiate the bullet
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(rot));

        // Check if the player is playerID1 or playerID2
        if (myPlayer._playerID == 1 || myPlayer._playerID == 2)
        {
            // Check if the original bullet is null before instantiating the clone
            if (newProjectile != null)
            {
                // Instantiate a clone of the bullet for both playerID1 and playerID2
                GameObject newProjectileClone = Instantiate(newProjectile, transform.position, Quaternion.Euler(rot));
            }
        }

        // Destroy the original bullet
        if (newProjectile != null)
        {
            Destroy(newProjectile);
        }

        playASound(shootSound);

        remainingRounds -= 1;
        playerAmmoSlider.value = remainingRounds;
    }



    public void reload()
    {
        remainingRounds = maxRounds;
        playerAmmoSlider.value = remainingRounds;
        playASound(reloadSound);
    }

    void playASound(AudioClip playTheSound)
    {
        gunMuzzleAS.clip = playTheSound;
        gunMuzzleAS.Play();
    }
}
