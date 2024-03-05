using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float fallSpeed = 5f;

    private void Update()
    {
        //Move the object downwards
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
