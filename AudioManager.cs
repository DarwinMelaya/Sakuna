using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sound Effects")]
    public AudioSource sfxSource;
    public AudioClip collectSound;

    [Header("Background Music")]
    public AudioSource musicSource;

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

    public void PlayCollectSound()
    {
        sfxSource.PlayOneShot(collectSound);
    }
}
