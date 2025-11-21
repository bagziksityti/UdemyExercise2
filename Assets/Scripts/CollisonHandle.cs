using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonHandle : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {   
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                LoadNextLevel();
           
                    Debug.Log("You win!");
                break;
            
            default:
                ReloadLevel();              
                Debug.Log("You died!");
                break;
        }

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
    void ReloadLevel()                                          // just loads the current level
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }



}
