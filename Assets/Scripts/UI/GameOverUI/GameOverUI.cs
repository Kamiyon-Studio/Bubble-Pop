using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [Header("Texts")]
    public TextMeshProUGUI bubblePoppedText;
    public TextMeshProUGUI scoreText;

    [Header("buttons")]
    public Button retryButton;
    public Button mainMenuButtonl;

    private int score;
    private int bubblePopped;

    private void Awake() {
        bubblePopped = ScoreManager.Instance.GetBubblePopped();
        score = ScoreManager.Instance.GetGameScore();

        UpdateTexts();
    }

    private void UpdateTexts() {
        scoreText.text = score.ToString();
        bubblePoppedText.text = "x " + bubblePopped.ToString();
    }
}
