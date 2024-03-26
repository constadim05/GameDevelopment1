using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAxisMovement : MonoBehaviour

{
    public float speed = 5f; // Speed at which the object moves along the X-axis

    // Update is called once per frame
    void Update()
    {
        // Get input for horizontal axis
        float moveInput = Input.GetAxis("Horizontal");

        // Calculate movement only along X-axis
        Vector3 movement = new Vector3(moveInput, 0, 0);

        // Translate the object's position
        transform.Translate(movement * speed * Time.deltaTime);
    }
}