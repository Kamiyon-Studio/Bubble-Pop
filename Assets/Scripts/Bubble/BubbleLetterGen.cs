using TMPro;
using UnityEngine;

public class BubbleLetterGen : MonoBehaviour {
    
    [Header("TextTMP")]
    [SerializeField] private TextMeshProUGUI _textTMP;


    private void Awake() {
        GenereateLetter();
    }

    private void GenereateLetter() {
        char randomLetter = (char)Random.Range('A', 'Z' + 1);

        if (_textTMP != null) {
            _textTMP.text = randomLetter.ToString();
        } else {
            Debug.LogWarning("BubbleLetterGen: _textTMP is null!");
        }
    }

    public char GetLetter() {
        return _textTMP.text[0];
    }
}
