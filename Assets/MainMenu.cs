using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
       public void NewGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void QuitGame()
    {
            // Quit the application
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the editor
    #else
        Application.Quit(); // Quits the built application
    #endif
    }
}
