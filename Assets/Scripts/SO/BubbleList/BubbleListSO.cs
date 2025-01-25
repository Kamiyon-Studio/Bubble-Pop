using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BubbleListSO", menuName = "ScriptableObjects/BubbleListSO", order = 0)]
public class BubbleListSO : ScriptableObject {
    public List<BubbleSO> bubbleObject;
}
