using System.Collections.Generic;
using UnityEngine;

public class HeartUIAnimator : MonoBehaviour {
    private const string HEART_BREAK = "HeartBreak";

    private Animator heartAnimator;

    private void Awake() {
        heartAnimator = GetComponent<Animator>();
    }

    public void PlayHeartBreak() {
        heartAnimator.SetTrigger(HEART_BREAK);
    }

    public void DestroyHeart() {
        Destroy(gameObject);
    }
}
