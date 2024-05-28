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

    // Audio info
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

        if (myPlayer._playerID == 1 && Input.GetKey(KeyCode.Space) && nextBullet < Time.time && remainingRounds > 0)
        {
            Fire(myPlayer);
        }

        if (myPlayer._playerID == 2 && Input.GetKey(KeyCode.M) && nextBullet < Time.time && remainingRounds > 0)
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
        Instantiate(projectile, transform.position, Quaternion.Euler(rot));

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

    public void addAmmo(int amount)
    {
        remainingRounds = Mathf.Min(remainingRounds + amount, maxRounds);
        playerAmmoSlider.value = remainingRounds;
    }

    void playASound(AudioClip playTheSound)
    {
        gunMuzzleAS.clip = playTheSound;
        gunMuzzleAS.Play();
    }
}
