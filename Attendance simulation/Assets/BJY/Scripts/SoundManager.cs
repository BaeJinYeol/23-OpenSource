using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgSound;
    public AudioClip[] bglist;
    [SerializeField] private Slider sound_slider;
    public float bgVolume = 0.5f;
    public float sfxVolume = 0.5f;

    private AudioSource city_sound;
    private StarterAssets.ThirdPersonController player_Controller;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayBgSound(bglist[0]);
        sound_slider = GameObject.Find("Canvas").transform.Find("Audio").transform.Find("Slider").GetComponent<Slider>();
    }

    public void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Room")
        {
            player_Controller = GameObject.Find("Player").GetComponent<StarterAssets.ThirdPersonController>();
            player_Controller.FootstepAudioVolume = bgVolume;
            bgSound.Stop();

        }
        else if (arg0.name == "OverScene")
        {
            PlayBgSound(bglist[0]);
            bgSound.volume = bgVolume;
        }
        else if (arg0.name == "MainMenuScene")
        {
            sound_slider = GameObject.Find("Canvas").transform.Find("Audio").transform.Find("Slider").GetComponent<Slider>();
        }
    }

    public void SetBgVolume()
    {
        bgSound.volume = bgVolume = sound_slider.value;
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
