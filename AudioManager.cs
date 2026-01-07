using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sound Effects")]
    public AudioSource sfxSource;
    public AudioClip collectSound;

    [Header("Background Music")]
    public AudioSource musicSource;

    [Header("Scene Music")]
    public AudioClip[] sceneMusic; 
    // Index must match Build Settings scene index

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
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic(scene.buildIndex);
    }

    void PlaySceneMusic(int sceneIndex)
    {
        if (sceneIndex < sceneMusic.Length && sceneMusic[sceneIndex] != null)
        {
            musicSource.Stop();
            musicSource.clip = sceneMusic[sceneIndex];
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayCollectSound()
    {
        sfxSource.PlayOneShot(collectSound);
    }
}
