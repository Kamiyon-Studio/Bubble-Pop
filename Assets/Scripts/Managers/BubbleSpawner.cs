using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour {

    public static BubbleSpawner Instance { get; private set; }

    [SerializeField] private SpawnLocationSO spawnLocation;
    [SerializeField] private GameObject bubbleContainer;
    [SerializeField] private BubbleListSO bubbleListSO;

    [SerializeField] private float spawnInterval = 1f;

    private int bubbleCount = 0;
    private int maxBubbles = 30;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("BubbleSpawner: There is already a BubbleSpawner in the scene!");
            Destroy(gameObject);
        }
    }

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void Update() {
        UpdateSpawnInterval();
    }
    
    /// <summary>
    /// Starts the game by setting the state to CountDownToStart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameInProgress()) {
            StartCoroutine(SpawnBubblesInterval());
        } else {
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// Spawns bubbles at regular intervals
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnBubblesInterval() {
        while (bubbleCount < maxBubbles) {
            SpawnBubble();
            bubbleCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /// <summary>
    /// Spawns a bubble at a random spawn point in the scene
    /// </summary>
    private void SpawnBubble() {
        if (spawnLocation.spawnLocation != null) {
            Transform[] childeTransforms = spawnLocation.spawnLocation.GetComponentsInChildren<Transform>();

            List<Transform> spawnPoints = new List<Transform>();
            foreach (Transform child in childeTransforms) {
                if (child != spawnLocation.spawnLocation.transform) {
                    spawnPoints.Add(child);
                }
            }

            if (spawnPoints.Count > 0) {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                GameObject selectedBubble = GetRandomBubbleByWeight();

                if (selectedBubble != null) {
                    Instantiate(selectedBubble, randomSpawnPoint.position, Quaternion.identity, bubbleContainer.transform);
                }
            } else {
                Debug.LogError("BubbleSpawner: No spawn points found!");
            }
        } else {
            Debug.LogError("BubbleSpawner: No spawn location found!");
        }
    }

    /// <summary>
    /// Returns a random bubble type from the list
    /// </summary>
    /// <returns></returns>
    private GameObject GetRandomBubbleByWeight() {
        if (bubbleListSO == null || bubbleListSO.bubbleObject == null || bubbleListSO.bubbleObject.Count == 0) {
            Debug.Log("BubbleSpawner: No bubbles found in the list!");
            return null;
        }

        List<float> weights = new List<float> { 50f, 30f, 20f };

        if (weights.Count != bubbleListSO.bubbleObject.Count) {
            Debug.Log("BubbleSpawner: The number of weights does not match the number of bubbles in the list!");
            return null;
        }

        float totalWeight = 0f;
        foreach (float weight in weights) {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        for (int i = 0; i < bubbleListSO.bubbleObject.Count; i++) {
            cumulativeWeight += weights[i];
            if (randomValue <= cumulativeWeight) {
                return bubbleListSO.bubbleObject[i].bubblePrefab;
            }
        }

        return null;
    }

    /// <summary>
    /// Updates the spawn interval based on the game timer
    /// </summary>
    private void UpdateSpawnInterval() {
        if (GameManager.Instance.IsGameInProgress()) {
            float currentTimer = GameManager.Instance.GetGameTimer();

            if (currentTimer > 50f && currentTimer <= 60f) {
                spawnInterval = 2f;
            } else if (currentTimer > 40f && currentTimer <= 50f) {
                spawnInterval = 1.8f;
            } else if (currentTimer > 30f && currentTimer <= 40f) {
                spawnInterval = 1.5f;
            } else if (currentTimer > 20f && currentTimer <= 30f) {
                spawnInterval = 1.3f;
            } else if (currentTimer > 10f && currentTimer <= 20f) {
                spawnInterval = 1f;
            } else if (currentTimer > 0f && currentTimer <= 10f) {
                spawnInterval = 0.8f;
            }
        }
    }

    /// <summary>
    /// Decrements the bubble count
    /// </summary>
    public void DecrementBubbleCount() {
        if (bubbleCount > 0) {
            bubbleCount--;
        }
    }
}
