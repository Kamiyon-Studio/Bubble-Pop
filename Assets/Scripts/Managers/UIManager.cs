using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject BottomUIPanel;

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
        GameManager.Instance.OnHealthDecrement += UpdateHealthUI;
    }

    private void Update() {
        UpdateTimerCounterText();
    }


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
