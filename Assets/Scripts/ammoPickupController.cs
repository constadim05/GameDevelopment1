using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickupController : MonoBehaviour
{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if(other.tag == "Player")
//        {
//            other.GetComponentInChildren<fireBullet>().reload();
//            Destroy(transform.root.gameObject);
//        }
//    }
//}

public float ammoAmount;
public AudioClip ammoPickupSound;

// Start is called before the first frame update
void Start()
{

}

// Update is called once per frame
void Update()
{

}

void OnTriggerEnter(Collider other)
{
    if (other.tag == "Player")
    {
        other.GetComponent<playerHealth>().addHealth(ammoAmount);
        Destroy(transform.root.gameObject);
        AudioSource.PlayClipAtPoint(ammoPickupSound, transform.position, 0.15f);
    }
}
}