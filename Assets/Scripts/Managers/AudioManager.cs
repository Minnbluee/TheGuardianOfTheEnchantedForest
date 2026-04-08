using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Música")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header("Efectos de Sonido")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip collectDiamondSFX;
    [SerializeField] private AudioClip playerHurtSFX;
    [SerializeField] private AudioClip attackSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ValidateComponents();
    }

    private void ValidateComponents()
    {
        if (musicSource == null)
            Debug.LogError("Falta asignar el AudioSource de música en el Inspector.");
        if (sfxSource == null)
            Debug.LogError("Falta asignar el AudioSource de SFX en el Inspector.");
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null) return;
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        if (musicSource != null)
            musicSource.volume = volume;
    }

    public void PlayCollectDiamond()
    {
        PlaySFX(collectDiamondSFX);
    }

    public void PlayPlayerHurt()
    {
        PlaySFX(playerHurtSFX);
    }

    public void PlayAttack()
    {
        PlaySFX(attackSFX);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        if (sfxSource != null)
            sfxSource.volume = volume;
    }
}