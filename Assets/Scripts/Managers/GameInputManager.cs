using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour {

    public static GameInputManager Instance { get; private set; }

    public event EventHandler OnSpacePressed;

    private PlayerInputActions playerInputActions;
    private Dictionary<Key, InputAction> keyActions = new Dictionary<Key, InputAction>();

    private char letterPressed;


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

        playerInputActions.Keyboard.Space.performed += ctx => { OnSpacePressed?.Invoke(this, EventArgs.Empty); };
        CreateAlphabetActions();
    }


    private void Update() {
        CheckKeyInputHeldDown();
    }

    private void OnDisable() {
        playerInputActions.Disable();
    }

    /// <summary>
    /// Checks if a key is being held down
    /// </summary>
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

    /// <summary>
    /// Creates a new InputAction for each letter in the alphabet
    /// </summary>
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