using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootFireBall : MonoBehaviour
{
    public float damage;
    public float speed;

    Rigidbody myRB;

    
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();

        if(transform.rotation.y > 0)
            myRB.AddForce(Vector3.right * speed, ForceMode.Impulse);
        else 
           myRB.AddForce(Vector3.right * -speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            myRB.velocity = Vector3.zero;
            EnemyHealth theEnemyHealth = other.GetComponent<EnemyHealth>();

            if(theEnemyHealth != null)
            {
                theEnemyHealth.addDamage(damage);
                
            }
            Destroy(gameObject);
        }
    }
}
