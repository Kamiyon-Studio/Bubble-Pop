using UnityEngine;

public class BubbleSpawnSFX : MonoBehaviour {
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManagers_OnStateChanged;
    }

    private void GameManagers_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameInProgress()) {
            audioSource.Play();
        }
    }
}
