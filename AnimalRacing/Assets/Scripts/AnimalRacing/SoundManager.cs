using UnityEngine;
using System.Collections;

public class SoundManager{

    private static GameObject bgMusic;
    private static GameObject btSound;

    public static void LoadBgMusic(string fileName, bool isChangeMusic)
    {
        if (!isMusic) return;
        if (isChangeMusic)
        {
            if (bgMusic != null)
            {
                Object.Destroy(bgMusic);
                bgMusic = null;
            }
        }
        if (bgMusic == null)
        {
            bgMusic = new GameObject("BgMusic");
            AudioSource audio = bgMusic.AddComponent<AudioSource>();
            audio.clip = Resources.Load<AudioClip>(fileName);
            audio.loop = true;
            audio.Play();
            Object.DontDestroyOnLoad(bgMusic);
        }
    }

    public static void playSound(string fileName)
    {
        if (!isSound) return;
        GameObject sound = new GameObject("Sound");
        AudioSource audio = sound.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>(fileName);
        audio.Play();
        Object.Destroy(sound, 1);
    }

    public static void playSoundLong(string fileName)
    {
        if (!isSound) return;
        GameObject sound = new GameObject("Sound");
        AudioSource audio = sound.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>(fileName);
        audio.Play();
    }

    public static void playSoundLong(string fileName, float duration)
    {
        if (!isSound) return;
        GameObject sound = new GameObject("Sound");
        AudioSource audio = sound.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>(fileName);
        audio.Play();
        Object.Destroy(sound, duration);
    }

    public static void playButtonSound()
    {
        //playSound("Sounds/click");
        if (!isSound) return;
        if (btSound != null)
        {
            btSound.GetComponent<AudioSource>().Play();
        }
        else
        {
            btSound = new GameObject("BtSound");
            AudioSource audio = btSound.AddComponent<AudioSource>();
            audio.clip = Resources.Load<AudioClip>("Sounds/click");
            audio.Play();
            Object.DontDestroyOnLoad(btSound);
        }
    }

    public static void stopMusic()
    {
        if(bgMusic != null)
            Object.Destroy(bgMusic);
    }

    public static void PauseMusic()
    {
        if (isMusic)
        {
            if (bgMusic != null)
            {
                bgMusic.GetComponent<AudioSource>().Pause();
            }
        }
    }

    public static void ResumeMusic(string fileName)
    {
        if (isMusic)
        {
            if (bgMusic != null)
            {
                bgMusic.GetComponent<AudioSource>().Play();
            }
            else
            {
                LoadBgMusic(fileName, true);
            }
        }
    }

    public static bool isMusic = true;
    public static bool isSound = true;
}
