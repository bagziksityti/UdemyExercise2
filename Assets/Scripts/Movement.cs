using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float speed = 10f;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationStrenght = 10f;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
       float rotationInput =  rotation.ReadValue<float>();
        Debug.Log(rotationInput);

        if (rotationInput < 0f)
        {
            ApplyRotation(1);
        }
        else if (rotationInput > 0f)
        {
            ApplyRotation(-1);
        }

    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationStrenght * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }

   

}
