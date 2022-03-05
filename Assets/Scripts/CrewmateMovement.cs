using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CrewmateMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    float xDir, zDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        zDir = Input.GetAxisRaw("Vertical");
        xDir = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rb.velocity = speed * new Vector3(xDir, 0, zDir);
        //rb.AddForce(speed * new Vector3(xDir, 0, zDir), ForceMode.Impulse);
    }
}
