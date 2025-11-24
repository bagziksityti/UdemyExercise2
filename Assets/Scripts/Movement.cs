using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float speed = 10f;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationStrenght = 10f;
    [SerializeField] AudioClip MainEngline;
    

    Rigidbody rb;           // class variable for rigidbody 
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();          // getting reference to the rigidbody ... audio source
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        thrust.Enable();                // Input Action is disabled by default so we need to enable it
        rotation.Enable();              
    }
    private void FixedUpdate()
    {
        ProcessTrhust();
        ProcessRotation();
        

    }

    private void ProcessTrhust()     
    {
       
        if (thrust.IsPressed())         // thrust.ReadValue<float>() > 0f (input action)
        {
            rb.AddRelativeForce(Vector3.up * speed * Time.fixedDeltaTime);   // adding force relative to the object
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(MainEngline);   // play the sound PlayOneShot does not interrupt itself
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
