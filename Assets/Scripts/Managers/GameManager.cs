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

    /// <summary>
    /// Initialize event listeners needed for the game loop to start
    /// </summary>
    private void Start() {
        GameInputManager.Instance.OnSpacePressed += GameInputManager_OnSpacePressed;
    }

    /// <summary>
    /// Starts the game by setting the state to CountDownToStart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GameInputManager_OnSpacePressed(object sender, EventArgs e) {
        if (state == State.WaitingToStart) {
            state = State.CountDownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Handle the game loop states and update the game timer
    /// </summary>
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

    /// <summary>
    /// Decrement the player health
    /// </summary>
    public void DecrementHealth() {
        Health--;
        OnHealthDecrement?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Check if the game is waiting to start
    /// </summary>
    /// <returns> True if the game is waiting to start</returns>
    public bool IsWaitingToStart() {
        return state == State.WaitingToStart;
    }

    /// <summary>
    /// Check if the game is in the count down to start
    /// </summary>
    /// <returns> True if the game is in the count down to start</returns>
    public bool IsCountDownToStart() {
        return state == State.CountDownToStart;
    }

    /// <summary>
    /// Check if the game is in progress
    /// </summary>
    /// <returns> True if the game is in progress</returns>
    public bool IsGameInProgress() {
        return state == State.GameInProgress;
    }

    /// <summary>
    /// Check if the game is over
    /// </summary>
    /// <returns> True if the game is over</returns>
    public bool IsGameOver() {
        return state == State.GameOver;
    }

    /// <summary>
    /// Get the game timer
    /// </summary>
    /// <returns> The game timer</returns>
    public float GetGameTimer() {
        return gameTimer;
    }
}
