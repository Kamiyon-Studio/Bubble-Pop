using UnityEngine;

public class BubbleTrigger : MonoBehaviour {

    [SerializeField] private BubbleSO BubbleSO;
    private BubbleLetterGen bubbleLetterGen;

    private void Awake() {
        bubbleLetterGen = GetComponent<BubbleLetterGen>();
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
                    Destroy(gameObject);
                }
            }
        }
    }
}
