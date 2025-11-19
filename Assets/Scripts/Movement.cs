using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float speed = 10f;
    [SerializeField] InputAction rotation;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void FixedUpdate()
    {
        ProcessTrhust();
        ProcessRotation();
        

    }

    private void ProcessTrhust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * speed * Time.fixedDeltaTime);
        }
    }

    private void ProcessRotation()
    {
       float rotationInput =  rotation.ReadValue<float>();
        Debug.Log(rotationInput);

        if (rotationInput < 0f)
        {
            transform.Rotate(Vector3.forward);
        }
        else if (rotationInput > 0f)
        {
            transform.Rotate(Vector3.back);
        }

    }
}
