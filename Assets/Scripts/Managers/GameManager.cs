using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnHealthDecrement;

    public enum State {
        WaitingToStart,
        CountDownToStart,
        GameInProgress,
        GameOver,
    }

    private State state;

    private int Health = 3;
    private float gameTimer = 60f;
    private float countdownTimer = 4f;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("GameManager: There is already a GameManager in the scene!");
            Destroy(gameObject);
        }

        state = State.WaitingToStart;
    }

    private void Start() {
        GameInputManager.Instance.OnSpacePressed += GameInputManager_OnSpacePressed;
    }

    private void GameInputManager_OnSpacePressed(object sender, EventArgs e) {
        if (state == State.WaitingToStart) {
            state = State.CountDownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                break;

            case State.CountDownToStart:
                Debug.Log("State: CountDownToStart");

                countdownTimer -= Time.deltaTime;
                if (countdownTimer <= 0f) {
                    state = State.GameInProgress;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameInProgress:
                Debug.Log("State: GameInProgress");

                gameTimer -= Time.deltaTime;
                if (gameTimer <= 0f) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                if (Health <= 0) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                Debug.Log("State: GameOver");
                break;
        }
    }

    public void DecrementHealth() {
        Health--;
        OnHealthDecrement?.Invoke(this, EventArgs.Empty);
    }

    public bool IsWaitingToStart() {
        return state == State.WaitingToStart;
    }

    public bool IsCountDownToStart() {
        return state == State.CountDownToStart;
    }

    public bool IsGameInProgress() {
        return state == State.GameInProgress;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetGameTimer() {
        return gameTimer;
    }
}
