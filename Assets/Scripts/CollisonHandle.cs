using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;



public class CollisonHandle : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;
    [SerializeField] AudioClip crashing;
    [SerializeField] AudioClip finishing;
    [SerializeField] ParticleSystem finishingParticles;
    [SerializeField] ParticleSystem crashingParticles;

    AudioSource audioSource;
    bool IsControllable = true;


    private void Start()
    {
             
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsControllable) // if we are not controllable do not process collisions
        {
            return;
        }

        switch (collision.gameObject.tag)               // Check the tag of the object we hit
        {
            case "Friendly":            // if it hits friendly tags
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartFinishSequance();      // if it hits finish tags
                    Debug.Log("You win!");
                break;
            
            default:                    // if it hits anything else it dies
                StartCrashSequance();                      
                Debug.Log("You died!");
                break;
        }

    }

    void StartFinishSequance()
    {
        // to do add particles
        IsControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(finishing);
        finishingParticles.Play();
        GetComponent<Movement>().enabled = false;       // turn off movement script on finish
        Invoke("LoadNextLevel", delayTime);
        
    }
    void StartCrashSequance()
    {
        // to do add particles
        IsControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashing);
        crashingParticles.Play();
        GetComponent<Movement>().enabled = false;       // turn off movement script on crash
        Invoke("ReloadLevel", delayTime);

    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;


        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    void ReloadLevel()                                          // just loads the current level did this first$$$
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    


}
