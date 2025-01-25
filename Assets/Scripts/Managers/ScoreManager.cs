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

    public void IncrementBubblePopped() {
        bubblePopped++;
        OnBubblePopped?.Invoke(this, EventArgs.Empty);
    }

    public void IncrementGameScore(int score) {
        gameScore += score;
    }

    public int GetBubblePopped() {
        return bubblePopped;
    }

    public int GetGameScore() {
        return gameScore;
    }

}
