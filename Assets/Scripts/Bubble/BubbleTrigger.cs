using UnityEngine;

public class BubbleTrigger : MonoBehaviour {

    private char letter = 'E';
    private void OnTriggerEnter2D(Collider2D collision) {
        if (IsMouseOverCollider()) {
            Debug.Log("Mouse clicked inside the collider!");
        }
    }

    private bool IsMouseOverCollider() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return GetComponent<Collider2D>().OverlapPoint(mousePosition);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverCollider()) {
                if (GameInputManager.Instance.GetLetterPressed() == letter) {
                    Debug.Log("Mouse clicked inside the collider!");
                }
            }
        }
    }
}
