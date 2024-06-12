using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float rotSpeed;
    public Animator anim;
    Rigidbody rb;

    Vector3 input;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input.Normalize();

        //transform.position += input * speed * Time.deltaTime;

        if (input == Vector3.zero) anim.SetBool("walking", false);
        else anim.SetBool("walking", true);

        //look
        var lookDirection = Quaternion.LookRotation(input, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookDirection, rotSpeed);
    }
    private void FixedUpdate()
    {
        rb.position += input * speed * Time.deltaTime;
    }
}
