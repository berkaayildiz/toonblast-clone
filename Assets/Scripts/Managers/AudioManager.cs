using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The AudioManager class is a singleton that manages audio playback in the game.
/// It provides methods to play music and sound effects, which are identified by SoundID.
/// The class also manages a dictionary that maps SoundID to AudioClip for easy retrieval of audio clips.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    [Serializable]
    private class SoundIDClipPair
    {
        public SoundID SoundID;
        public AudioClip AudioClip;
    }

    [SerializeField] 
    private SoundIDClipPair[] soundIDClipPairs;

    [SerializeField] 
    private AudioSource musicSource;

    [SerializeField] 
    private AudioSource effectSource;

    private readonly Dictionary<SoundID, AudioClip> soundIDToClipMap = new();

    private void Start()
    {
        InitializeSoundIDToClipMap();
    }

    private void InitializeSoundIDToClipMap()
    {
        foreach (var soundIDClipPair in soundIDClipPairs)
        {
            soundIDToClipMap.Add(soundIDClipPair.SoundID, soundIDClipPair.AudioClip);
        }
    }

    private void PlayMusic(AudioClip audioClip, bool looping = true)
    {
        if (musicSource.isPlaying) return;

        musicSource.clip = audioClip;
        musicSource.loop = looping;
        musicSource.Play();
    }

    private void PlayEffect(AudioClip audioClip)
    {
        effectSource.PlayOneShot(audioClip);
    }

    public void PlayEffect(SoundID soundID)
    {
        if(soundID == SoundID.None) return;

        var audioClip = soundIDToClipMap[soundID];
        PlayEffect(audioClip);
    }
}
