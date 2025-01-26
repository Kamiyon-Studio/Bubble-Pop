using UnityEngine;

public class BubbleSFX : MonoBehaviour {

    [SerializeField] private BubbleSFXListSO bubbleSFXListSO;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// Plays a random bubble sfx when the bubble is popped
    /// </summary>
    public void PlayRandomAudio() {
        AudioClip randomClip = bubbleSFXListSO.BubbleSFX[Random.Range(0, bubbleSFXListSO.BubbleSFX.Count)];
        audioSource.PlayOneShot(randomClip);
    }

    /// <summary>
    /// Mutes the audio source
    /// </summary>
    public void MuteAudioSource() {
        audioSource.mute = true;
    }
}
