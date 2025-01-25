using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance { get; private set; }

    private enum State {
        WaitingToStart,
        CountDownToStart,
        GameInProgress,
        GameOver,
    }

    private State state;

    private int Health = 3;
    private float timer = 60f;
    private float countdownTimer = 3f;

    private bool timeRunning = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("GameManager: There is already a GameManager in the scene!");
            Destroy(gameObject);
        }

        state = State.WaitingToStart;
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                break;
            case State.CountDownToStart:
                break;
            case State.GameInProgress:
                break;
            case State.GameOver:
                break;
        }
    }

    public void DecrementHealth() {
        Health--;
        Debug.Log("Health: " + Health);
    }
}
