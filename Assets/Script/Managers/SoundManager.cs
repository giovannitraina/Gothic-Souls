using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public Sound[] suoni;
    public static SoundManager instance;
    void Awake()
    {
        if (instance == null)
      {
         instance = this;
      }
       
        foreach (Sound a in suoni)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            if (PlayerPrefs.HasKey("Sound"))
            {
                a.source.volume = PlayerPrefs.GetFloat("Sound");
            }
            else
            {
                a.source.volume = a.volume;
            }
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(suoni, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning(name + "missing");
            return;
        }
        s.source.Play();
    }

    public void Play(string name, bool music)
    {
        Sound s = Array.Find(suoni, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning(name + "missing");
            return;
        }
        if (music)
        {
            if (PlayerPrefs.HasKey("Music"))
            {
                s.source.volume = PlayerPrefs.GetFloat("Music");
            }
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(suoni, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning(name + "missing");
            return;
        }
        s.source.Stop();
    }

    public void ChangeVolume(string name, float volume)
    {
        Sound s = Array.Find(suoni, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning(name + "missing");
            return;
        }

        s.source.volume = volume;
    }
}
