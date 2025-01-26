using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour {
    public enum Scene {
        MainMenu,
        GameScene,
    }

    private static Scene targetScene;

    public static void LoadScene(Scene scene) {
        SceneLoaderManager.targetScene = scene;
        SceneManager.LoadScene(targetScene.ToString());
    }
}
