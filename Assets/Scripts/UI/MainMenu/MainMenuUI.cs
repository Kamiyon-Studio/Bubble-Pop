using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [Header("Buttons")]
    public Button playButton;
    public Button creditsButton;
    public Button quitButton;

    [Header("Panels")]
    public GameObject creaditsPanel;
    public Button creditsCloseButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            SceneLoaderManager.LoadScene(SceneLoaderManager.Scene.GameScene);
        });

        creditsButton.onClick.AddListener(() => {
            ShowCreditsPanel();
        });

        creditsCloseButton.onClick.AddListener(() => {
            HideCreditsPanel();
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    private void ShowCreditsPanel() {
        creaditsPanel.SetActive(true);
    }

    private void HideCreditsPanel() {
        creaditsPanel.SetActive(false);
    }
}
