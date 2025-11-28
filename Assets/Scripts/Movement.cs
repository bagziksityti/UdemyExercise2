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
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrustParticles2;
    [SerializeField] ParticleSystem rightThrustParticles3;
    

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        NewMethod();
    }

    private void NewMethod()
    {
        rb.AddRelativeForce(Vector3.up * speed * Time.fixedDeltaTime);   // adding force relative to the object (new Vector3(0,1,0) * speed * Time.fixedDeltaTime)
                                                                         // Time.fixedDeltaTime makes it frame rate independent



        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainEngline);   // play the sound PlayOneShot does not interrupt itself 
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
        leftThrustParticles2.Stop();
        rightThrustParticles3.Stop();
    }

    private void ProcessRotation()
    {
       float rotationInput =  rotation.ReadValue<float>();
        Debug.Log(rotationInput);

        if (rotationInput < 0f)
        {
            RotateRight();
        }
        else if (rotationInput > 0f)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }

    }
    private void RotateRight()
    {
        ApplyRotation(1);
        if (!rightThrustParticles3.isPlaying)
        {
            leftThrustParticles2.Stop();
            rightThrustParticles3.Play();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(-1);
        if (!leftThrustParticles2.isPlaying)
        {
            rightThrustParticles3.Stop();
            leftThrustParticles2.Play();
        }
    }

    private void StopRotating()
    {
        rightThrustParticles3.Stop();
        leftThrustParticles2.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
       rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationStrenght * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;  // unfreezing rotation so the physics system can take over
    }

    
   

}
