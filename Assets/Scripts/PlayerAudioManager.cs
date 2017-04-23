using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {

    [SerializeField]
    AudioClip stun;

    [SerializeField]
    AudioClip stun2;

    [SerializeField]
    AudioSource sfxAudioSource;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayStunStinger()
    {
        //TODO: blend out of previous stinger?
        sfxAudioSource.clip = stun;
        sfxAudioSource.loop = false;
        sfxAudioSource.Play();
    }

    public void PlayDeathStinger()
    {
        //TODO: blend out of previous stinger?
        sfxAudioSource.clip = stun;
        sfxAudioSource.loop = false;
        sfxAudioSource.Play();
    }
}
