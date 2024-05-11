using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttcka : MonoBehaviour
{
    public float damage;
    public float knockBack;
    public float knockBackRadius;
    public float meleeRate;

    float nextMelee;

    int shootableMask;

    Animator myAnim;
    playerController myPC;

    // Start is called before the first frame update
    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        myAnim = transform.root.GetComponent<Animator>();
        myPC = transform.root.GetComponent<playerController>();
        nextMelee = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float melee = Input.GetAxis("Fire2");
        if (melee > 0 && nextMelee < Time.time && !(myPC.getRunning()))
        {
            myAnim.SetTrigger("gunMelee");
            nextMelee = Time.time + meleeRate;

            // Do Damage
            Collider[] attacked = Physics.OverlapSphere(transform.position, knockBackRadius, shootableMask);
            foreach (Collider hit in attacked)
            {
                // Apply damage and knockback to the hit object
                // Add your damage and knockback logic here
            }
        }
    }
}
