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
                Debug.Log("You win!");
                break;
            case "fueld":
                Debug.Log("Ybumped to me wy");
                break;
            default:
                ReloadLevel(); 
                Debug.Log("You died!");
                break;
        }

    }
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }



}
