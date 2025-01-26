using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button playButton;
    public Button creditsButton;
    public Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            SceneLoaderManager.LoadScene(SceneLoaderManager.Scene.GameScene);
        });

        creditsButton.onClick.AddListener(() => {
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
