using UnityEngine;

public class BubbleSFX : MonoBehaviour
{
    [SerializeField] private BubbleSFXListSO bubbleSFXListSO;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayRandomAudio() {
        AudioClip randomClip = bubbleSFXListSO.BubbleSFX[Random.Range(0, bubbleSFXListSO.BubbleSFX.Count)];
        audioSource.PlayOneShot(randomClip);

    }
}
