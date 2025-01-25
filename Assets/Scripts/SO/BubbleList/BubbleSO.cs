using UnityEngine;

[CreateAssetMenu(fileName = "BubbleSO", menuName = "ScriptableObjects/BubbleSO", order = 2)]
public class BubbleSO : ScriptableObject {
    public GameObject bubblePrefab;
    public int scoreCount;
}
