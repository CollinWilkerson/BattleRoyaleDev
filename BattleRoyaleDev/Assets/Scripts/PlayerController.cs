using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Components")]
    public Rigidbody rig;

    private void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
    }

    private void Move()
    {
        //read key inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //compencates for object rotation
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;

        //physics based movement, no frame rate compensation needed
        rig.velocity = dir;
    }

    private void TryJump()
    {
        //raycast down and trigger if it hits an object
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1.5f))
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
