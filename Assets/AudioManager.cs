using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;
    
    [Header("Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
    
    [Header("SFX Clips")]
    public AudioClip buttonClickSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip cardAppearSFX;
    public AudioClip scorePointSFX;
    public AudioClip playerMoveSFX;

    private void Awake()
    {
        // Singleton pattern to persist between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Play main menu music
    public void PlayMainMenuMusic()
    {
        PlayMusic(mainMenuMusic);
    }

    // Play gameplay music
    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    // Generic method to play music
    private void PlayMusic(AudioClip musicClip)
    {
        if (musicSource.clip == musicClip && musicSource.isPlaying)
            return;

        musicSource.clip = musicClip;
        musicSource.Play();
    }

    // Play SFX methods
    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSFX);
    }

    public void PlayWinSound()
    {
        PlaySFX(winSFX);
    }

    public void PlayLoseSound()
    {
        PlaySFX(loseSFX);
    }

    public void PlayCardAppearSound()
    {
        PlaySFX(cardAppearSFX);
    }

    public void PlayScorePointSound()
    {
        PlaySFX(scorePointSFX);
    }

    public void PlayPlayerMoveSound()
    {
        PlaySFX(playerMoveSFX);
    }

    // Generic method to play SFX
    private void PlaySFX(AudioClip sfxClip)
    {
        if (sfxClip != null)
            sfxSource.PlayOneShot(sfxClip);
    }

    // Control volume methods
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    // Pause/Resume music
    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
}