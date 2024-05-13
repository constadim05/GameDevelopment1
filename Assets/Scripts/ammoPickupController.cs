using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickupController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.name == "soldier_1" || other.gameObject.name == "soldier_2")
            {
                other.GetComponentInChildren<fireBullet>().Reload();
                Destroy(transform.root.gameObject);
            }
        }
    }

}

