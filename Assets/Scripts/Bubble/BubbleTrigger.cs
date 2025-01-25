using TMPro;
using UnityEngine;

public class BubbleTrigger : MonoBehaviour {

    private const string IS_POPPED = "isPopped";

    [SerializeField] private BubbleSO BubbleSO;
    [SerializeField] private GameObject bubbleText;
    private BubbleLetterGen bubbleLetterGen;

    private Animator bubbleAnimator;
    private BubbleSFX bubbleSFX;

    private bool hasCollided = false;

    private void Awake() {
        bubbleLetterGen = GetComponent<BubbleLetterGen>();
        bubbleAnimator = GetComponent<Animator>();
        bubbleSFX = GetComponent<BubbleSFX>();
    }

    private bool IsMouseOverCollider() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return GetComponent<Collider2D>().OverlapPoint(mousePosition);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (IsMouseOverCollider()) {
                if (GameInputManager.Instance.GetLetterPressed() == bubbleLetterGen.GetLetter()) {
                    BubbleSpawner.Instance.DecrementBubbleCount();
                    ScoreManager.Instance.IncrementGameScore(BubbleSO.scoreCount);
                    ScoreManager.Instance.IncrementBubblePopped();

                    PlayDestroyAnim();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<BubbleBoundaryManager>() != null && !hasCollided) {
            BubbleSpawner.Instance.DecrementBubbleCount();
            GameManager.Instance.DecrementHealth();
            bubbleSFX.PlayRandomAudio();
            PlayDestroyAnim();

            GetComponent<CircleCollider2D>().enabled = false;
            hasCollided = true;
        }
    }

    /// <summary>
    /// Play destroy animation that is used when the bubble popped
    /// </summary>
    private void PlayDestroyAnim() {
        bubbleText.SetActive(false);
        bubbleAnimator.SetTrigger(IS_POPPED);
    }

    public void DestroyBubble() {
        Destroy(gameObject);
    }
}
