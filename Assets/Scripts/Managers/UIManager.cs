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
    }

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
}
