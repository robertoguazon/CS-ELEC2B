using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

    [SerializeField]
    private AudioClip dropChipAudioClip;

    private static AudioSource _audioSource;
    private static AudioClip _dropChipSound;

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
        _dropChipSound = dropChipAudioClip;
	}

    public static void PlayDropChip() {
        if (_audioSource.clip != _dropChipSound) {
            _audioSource.clip = _dropChipSound;
        }
        _audioSource.Play();
    }
}
