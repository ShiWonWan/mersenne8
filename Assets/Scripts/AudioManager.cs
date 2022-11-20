using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] music;
    public AudioSource[] sfx;

    public int levelMusicToPlay;
    public AudioMixerGroup musicMixer, sfxMixer;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Play an specific music
    // @param int (Number of the music)
    // @return void
    public void PlayMusic(int musicToPlay)
    {
        foreach (AudioSource song in music)
        {
            song.Stop();
        }

        music[musicToPlay].Play();
    }

    // Play an specific SFX sound
    // @param int (Number of the effect)
    // @return void
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }

    // Set music level from the UI
    // @param none
    // @return void
    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicVol", UIManager.instance.musicVolSlier.value);
    }

    // Set SFX level from the UI
    // @param none
    // @return void
    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SFXVol", UIManager.instance.sfxVolSlider.value);
    }

}
