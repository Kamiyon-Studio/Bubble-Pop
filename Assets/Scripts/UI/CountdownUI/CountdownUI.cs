using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countDowntext;

    public void SetText(string newText) {
        countDowntext.text = newText;
    }
}
