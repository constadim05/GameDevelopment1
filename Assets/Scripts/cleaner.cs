using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleaner : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            playerHealth playerDead = other.GetComponent<playerHealth>();
            if (playerDead != null)
            {
                playerDead.makeDead();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
