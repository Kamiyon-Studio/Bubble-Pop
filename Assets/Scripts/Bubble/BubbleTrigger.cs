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

    /// <summary>
    /// Get the components needed for the bubble
    /// </summary>
    private void Awake() {
        bubbleLetterGen = GetComponent<BubbleLetterGen>();
        bubbleAnimator = GetComponent<Animator>();
        bubbleSFX = GetComponent<BubbleSFX>();
    }

    /// <summary>
    /// Checks if the mouse is over the collider of the bubble trigger
    /// and if the letter of the bubble trigger matches the letter of the mouse
    /// </summary>
    /// <returns></returns>
    private bool IsMouseOverCollider() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return GetComponent<Collider2D>().OverlapPoint(mousePosition);
    }

    private void Update() {
        PopBubble();
    }

    /// <summary>
    /// Checks if the mouse is clicked and if the letter of the bubble matches the letter of the mouse
    /// pops the bubble
    /// </summary>
    private void PopBubble() {
        if (GameManager.Instance.IsGameInProgress()) {
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
        } else {
            bubbleSFX.MuteAudioSource();
            PlayDestroyAnim();
        }
    }

    /// <summary>
    /// Checks if the bubble has collided with the bubble boundary
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<BubbleBoundaryManager>() != null && !hasCollided) {
            BubbleSpawner.Instance.DecrementBubbleCount();
            GameManager.Instance.DecrementHealth();

            GetComponent<CircleCollider2D>().enabled = false;
            hasCollided = true;
            DestroyBubble();
        }
    }

    /// <summary>
    /// Play destroy animation that is used when the bubble popped
    /// </summary>
    private void PlayDestroyAnim() {
        bubbleSFX.PlayRandomAudio();
        bubbleText.SetActive(false);
        bubbleAnimator.SetTrigger(IS_POPPED);
    }

    /// <summary>
    /// Destroys the bubble
    /// </summary>
    public void DestroyBubble() {
        Destroy(gameObject);
    }
}
