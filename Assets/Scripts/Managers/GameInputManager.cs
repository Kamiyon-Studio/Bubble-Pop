using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour {

    public static GameInputManager Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    private char letterPressed;

    private Dictionary<Key, InputAction> keyActions = new Dictionary<Key, InputAction>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("GameInputManager: There is already a GameInputManager in the scene!");
            Destroy(gameObject);
        }

        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable() {
        playerInputActions.Enable();
        CreateAlphabetActions();
    }

    private void Update() {
        CheckKeyInputHeldDown();
        Debug.Log(letterPressed);
    }

    private void OnDisable() {
        playerInputActions.Disable();
    }

    private void CheckKeyInputHeldDown() {
        bool isLetterPressed = false;

        foreach (var kvp in keyActions) {
            if (kvp.Value.ReadValue<float>() > 0) {
                if (!isLetterPressed) {
                    letterPressed = kvp.Key.ToString()[0];
                    isLetterPressed = true;
                }
            }
        }

        if (!isLetterPressed) {
            letterPressed = '\0';
        }
    }

    private void CreateAlphabetActions() {
        for (char letter = 'A'; letter <= 'Z'; letter++) {
            var key = (Key)System.Enum.Parse(typeof(Key), letter.ToString());
            var action = new InputAction($"{letter}Hold", binding: $"<Keyboard>/{letter.ToString().ToLower()}");
            action.Enable();
            keyActions[key] = action;
        }
    }

    public char GetLetterPressed() {
        return letterPressed;
    }
}