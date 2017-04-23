using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviourSingleton<AudioManager> {
    [SerializeField]
    List<AudioClip> mainMusicLoops;
    int currentMusicClipIndex;
    //AudioClip currentMusicClip;
    

    [SerializeField]
    AudioClip menuMusic;

    [SerializeField]
    AudioSource musicAudioSource;

    [SerializeField]
    AudioSource sfxAudioSource;

    bool isGameStarted = false;
    bool isMenuStarted = false;

    // Use this for initialization
    void Awake ()
    {
        PlayMenuMusic();
        DontDestroyOnLoad(this.gameObject);
    }




    public void PlayGameplayMusic()
    {
        isGameStarted = true;
        isMenuStarted = false;
        Debug.Log("AUDIO: playing gameplayMusic");
    }

    public void PlayMenuMusic()
    {
        isGameStarted = false;
        isMenuStarted = true;
        Debug.Log("AUDIO: playingMenuMusic");
    }





    // Update is called once per frame
    void Update () {
        if (isGameStarted)
        {
            playMusicLoop();
        }
        else
        {
            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Stop();
                currentMusicClipIndex = 0;
            }

        }


        if (isMenuStarted)
        {
            playMenuLoop();
        }
        else
        {
            if (sfxAudioSource.isPlaying)
            {
                sfxAudioSource.Stop();
            }
        }
    }




    void playMenuLoop()
    {
        //TODO: blend out of previous stinger?
        if (!sfxAudioSource.isPlaying)
        {
            sfxAudioSource.clip = menuMusic;
            sfxAudioSource.loop = false;
            sfxAudioSource.Play();
        }
    }

    void playMusicLoop()
    {
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.clip = mainMusicLoops[currentMusicClipIndex];
            musicAudioSource.Play();
            musicAudioSource.loop = false;

            currentMusicClipIndex++;
            if (currentMusicClipIndex >= mainMusicLoops.Count)
                currentMusicClipIndex = 0;
        }
    }

    

}
