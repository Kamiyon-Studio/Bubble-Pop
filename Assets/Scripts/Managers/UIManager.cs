using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject pressStartUI;
    public GameObject countDownUI;
    public GameObject BottomUIPanel;
    public GameObject gameOverUI;

    [Header("UI Bottom Panel Elements")]
    public TextMeshProUGUI poppedBubbleCounterText;
    public TextMeshProUGUI scoreCounterText;
    public TextMeshProUGUI timerCounterText;

    public List<GameObject> heartObjectsUI = new List<GameObject>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("UIManager: There is already a UIManager in the scene!");
            Destroy(gameObject);
        }
    }

    private void Start() {
        ScoreManager.Instance.OnBubblePopped += ScoreManager_OnBubblePopped;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        GameManager.Instance.OnHealthDecrement += UpdateHealthUI;

        EnablePressStartUI();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        EnablePressStartUI();
        EnableCountDownUI();
        EnableGameOverUI();
    }

    private void Update() {
        UpdateTimerCounterText();
    }

    // ------------------------------- PressStartUI -------------------------------
    private void EnablePressStartUI() {
        if (GameManager.Instance.IsWaitingToStart()) {
            pressStartUI.SetActive(true);
        } else {
            pressStartUI.SetActive(false);
        }
    }

    // ------------------------------- CountDownUI -------------------------------

    private void EnableCountDownUI() {
        if (GameManager.Instance.IsCountDownToStart()) {
            countDownUI.SetActive(true);
        } else {
            countDownUI.SetActive(false);
        }
    }

    // ------------------------------- GameOverUI -------------------------------
    private void EnableGameOverUI() {
        if (GameManager.Instance.IsGameOver()) {
            gameOverUI.SetActive(true);
        } else {
            gameOverUI.SetActive(false);
        }
    }

    // ------------------------------- Bottome UI -------------------------------

    /// <summary>
    /// Score Counter UI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ScoreManager_OnBubblePopped(object sender, System.EventArgs e) {
        UpdatePoppedBubbleCounterText();
        UpdateScoreCounterText();
    }

    private void UpdatePoppedBubbleCounterText() {
        poppedBubbleCounterText.text = " x " + ScoreManager.Instance.GetBubblePopped().ToString();
    }

    private void UpdateScoreCounterText() {
        scoreCounterText.text = ScoreManager.Instance.GetGameScore().ToString();
    }


    /// <summary>
    /// Timer Counter UI
    /// </summary>
    private void UpdateTimerCounterText() {
        if (GameManager.Instance.IsGameInProgress()) {
            timerCounterText.text = GameManager.Instance.GetGameTimer().ToString("F0");
        };
    }


    /// <summary>
    /// Health UI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateHealthUI(object sender, System.EventArgs e) {
        if (heartObjectsUI.Count > 0) {
            GameObject heartToDestroy = heartObjectsUI[0];

            HeartUIAnimator heartUIAnimator = heartToDestroy.GetComponent<HeartUIAnimator>();

            if (heartUIAnimator != null) {
                heartUIAnimator.PlayHeartBreak();
            }

            heartObjectsUI.Remove(heartToDestroy);
        }
    }

}
