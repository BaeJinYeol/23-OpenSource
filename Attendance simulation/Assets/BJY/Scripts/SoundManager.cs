using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgSound;
    public AudioClip[] bglist;
    public float bgVolume = 0.1f;
    public float sfxVolume = 0.1f;

    public override void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
                PlayBgSound(bglist[i]);
        }
    }

    public void SetBgVolume(float volume)
    {
        bgSound.volume = bgVolume = volume;
    }
    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
    }

    public void PlaySFXSound(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.volume = sfxVolume;
        audiosource.Play();

        Destroy(go, clip.length);
    }

    public void PlayBgSound(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = bgVolume;
        bgSound.Play();
    }
}
