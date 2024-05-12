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
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.GetComponentInChildren<fireBullet>().reload();
            Destroy(transform.root.gameObject);
        }
    }
}
