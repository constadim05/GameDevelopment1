using UnityEngine;

public class meleeAttack2 : MonoBehaviour
{
    public float damage;
    public float knockBack;
    public float knockBackRadius;
    public float meleeRate;

    float nextMelee;

    int shootableMask;

    Animator myAnim;
    playerController2 myPC;

    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        myAnim = transform.root.GetComponent<Animator>();
        myPC = transform.root.GetComponent<playerController2>();
        nextMelee = 0f;
    }

    void FixedUpdate()
    {
        // Remove the input detection for "Fire2_2"
        // float melee = Input.GetAxis("Fire2_2");
        // if (melee > 0 && nextMelee < Time.time && !(myPC.getRunning()))
        // {
        //     myAnim.SetTrigger("gunMelee");
        //     nextMelee = Time.time + meleeRate;

        //     //Do Damage
        //     Collider[] attacked = Physics.OverlapSphere(transform.position, knockBackRadius, shootableMask);
        // }
    }
}
