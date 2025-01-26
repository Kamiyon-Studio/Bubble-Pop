using UnityEngine;

public class BubbleBoundaryManager : MonoBehaviour {
/// <summary>
/// Base class used by the bubble trigger to detect if the bubble has collided with the bubble boundary
/// </summary>
/// 
    public static BubbleBoundaryManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("BubbleBoundaryManager: There is already a BubbleBoundaryManager in the scene!");
            Destroy(gameObject);
        }
    }
}
