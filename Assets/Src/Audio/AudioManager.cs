using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.mixer;
        }       
    }

    void Start()
    {
        Play("Theme");    
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Audio could not be played. Unknown audio name: " + sound.name);
            return;
        }
        sound.source.Play();
    }
    
    public void Play(string name, float volume)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Audio could not be played. Unknown audio name: " + sound.name);
            return;
        }
        sound.source.PlayOneShot(sound.source.clip, volume);    
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Audio could not be stopped. Unknown audio name: " + sound.name);
            return;
        }

        if (sound.source.isPlaying)
        {
            sound.source.Stop();    
        }
        
    }
}
