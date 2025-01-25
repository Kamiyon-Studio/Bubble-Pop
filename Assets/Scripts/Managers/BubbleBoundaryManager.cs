using UnityEngine;

public class BubbleBoundaryManager : MonoBehaviour {
    public static BubbleBoundaryManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("BubbleBoundaryManager: There is already a BubbleBoundaryManager in the scene!");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<BubbleTrigger>() != null) {
            BubbleSpawner.Instance.DecrementBubbleCount();
            GameManager.Instance.DecrementHealth();
        }
    }
}
