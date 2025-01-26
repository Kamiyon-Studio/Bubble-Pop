using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance { get; private set; }

    public event EventHandler OnBubblePopped;

    private int bubblePopped = 0;
    private int gameScore = 0;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("ScoreManager: There is already a ScoreManager in the scene!");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Increment the number of bubbles popped.
    /// </summary>
    public void IncrementBubblePopped() {
        bubblePopped++;
        OnBubblePopped?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Increment the game score
    /// </summary>
    /// <param name="score"></param>
    public void IncrementGameScore(int score) {
        gameScore += score;
    }

    /// <summary>
    /// Get the number of bubbles popped
    /// </summary>
    /// <returns> The number of bubbles popped</returns>
    public int GetBubblePopped() {
        return bubblePopped;
    }

    /// <summary>
    /// Get the game score
    /// </summary>
    /// <returns> The game score</returns>
    public int GetGameScore() {
        return gameScore;
    }

}
