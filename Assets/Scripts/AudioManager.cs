using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // This makes the script accessible from anywhere
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource; // The first Audio Source (for BGM)
    public AudioSource sfxSource; // The second Audio Source (for SFX)

    [Header("Audio Clips")]
    public AudioClip menuSong;
    public AudioClip themeSong;
    public AudioClip jumpSound;
    public AudioClip fallDeadSound;
    public AudioClip spikeDeadSound;
    public AudioClip winSound;
    public AudioClip swooshSound;
    public AudioClip platformCrumbleSound;
    public AudioClip gameOverSound;
    public AudioClip buttonClickSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        // Assign the MENU song to the BGM source and play it
        bgmSource.clip = menuSong; 
        bgmSource.loop = true; 
        bgmSource.Play();
    }

    public void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            sfxSource.PlayOneShot(jumpSound);
        }
    }
    
    public void PlayFallDeadSound()
    {
        if (fallDeadSound != null)
        {
            sfxSource.PlayOneShot(fallDeadSound);
        }
    }

    public void PlaySpikeDeadSound()
    {
        if (spikeDeadSound != null)
        {
            sfxSource.PlayOneShot(spikeDeadSound);
        }
    }

    public void PlayWinSound()
    {
        if (winSound != null)
        {
            sfxSource.PlayOneShot(winSound);
        }
    }
    public void PlaySwooshSound()
    {
        if (swooshSound != null)
        {
            sfxSource.PlayOneShot(swooshSound);
        }
    }

    public void PlayPlatformCrumbleSound()
    {
        if (platformCrumbleSound != null)
        {
            sfxSource.PlayOneShot(platformCrumbleSound);
        }
    }
    public void PlayGameOverSound()
    {
        if (gameOverSound != null)
        {
            sfxSource.PlayOneShot(gameOverSound);
        }
    }
    public void RestartThemeMusic()
    {
        // Stops the music and plays it again from the beginning
        if (bgmSource != null)
        {
            bgmSource.Stop();
            bgmSource.Play();
        }
    }

    public void StopThemeMusic()
    {
        // Stops the music
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }
    public void SwitchToLevelMusic()
    {
        // Stops, switches clip to level song, and plays
        if (bgmSource != null && themeSong != null && bgmSource.clip != themeSong)
        {
            bgmSource.Stop();
            bgmSource.clip = themeSong;
            bgmSource.Play();
        }
    }

    public void SwitchToMenuMusic()
    {
        // Stops, switches clip to menu song, and plays
        if (bgmSource != null && menuSong != null && bgmSource.clip != menuSong)
        {
            bgmSource.Stop();
            bgmSource.clip = menuSong;
            bgmSource.Play();
        }
    }
    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            sfxSource.PlayOneShot(buttonClickSound);
        }
    }
}