using TMPro;
using UnityEngine;

public class BubbleLetterGen : MonoBehaviour {
    
    [Header("TextTMP")]
    [SerializeField] private TextMeshProUGUI _textTMP;

    private void Awake() {
        GenereateLetter();
    }

    /// <summary>
    /// Generates a random letter and sets it to the textTMP
    /// </summary>
    private void GenereateLetter() {
        char randomLetter = (char)Random.Range('A', 'Z' + 1);

        if (_textTMP != null) {
            _textTMP.text = randomLetter.ToString();
        } else {
            Debug.LogWarning("BubbleLetterGen: _textTMP is null!");
        }
    }

    /// <summary>
    /// Returns the letter of the textTMP
    /// </summary>
    /// <returns>The letter of type char</returns>
    public char GetLetter() {
        return _textTMP.text[0];
    }
}
