using System;
using UnityEngine;
using UnityEngine.SceneManagement;



public class CollisonHandle : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;

    private void OnCollisionEnter(Collision collision)
    {   
        switch (collision.gameObject.tag)
        {
            case "Friendly":            // if it hits friendly tags
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                LoadNextLevel();
           
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
        // to do add sfx and particles
        GetComponent<Movement>().enabled = false;       // turn off movement script on finish
        Invoke("LoadNextLevel", delayTime);
    }
    void StartCrashSequance()
    {
        // to do add sfx and particles
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
