using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    // Public AudioClips go below
    public AudioClip sfxSandShuffle;
    public AudioClip sfxPickup;
    public AudioClip sfxWaves;

    private void Start() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void PlayAudio(AudioClip audio) {
        AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position);
    }
	
}
