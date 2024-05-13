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


    // Start is called before the first frame update
    void Awake()
    {
        nextBullet = 0f;
        remainingRounds = startingRounds;
        playerAmmoSlider.maxValue = maxRounds;
        playerAmmoSlider.value = remainingRounds;
        gunMuzzleAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "soldier_1" && Input.GetKeyDown(KeyCode.Space) && nextBullet < Time.time && remainingRounds > 0)
        {
            Fire();
        }

        if (gameObject.name == "soldier_2" && Input.GetKeyDown(KeyCode.M) && nextBullet < Time.time && remainingRounds > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        nextBullet = Time.time + timeBetweenBullets;
        Vector3 rot;
        if (GetComponent<playerController>().GetFacing() == -1f)
        {
            rot = new Vector3(0, -90, 0);
        }
        else rot = new Vector3(0, 90, 0);

        Instantiate(projectile, transform.position, Quaternion.Euler(rot));

        PlayASound(reloadSound);

        remainingRounds -= 1;
        playerAmmoSlider.value = remainingRounds;
    }

    public void Reload()
    {
        remainingRounds = maxRounds;
        playerAmmoSlider.value = remainingRounds;
        PlayASound(reloadSound);
    }

    void PlayASound(AudioClip playTheSound)
    {
        gunMuzzleAS.clip = shootSound;
        gunMuzzleAS.Play();
    }
}
