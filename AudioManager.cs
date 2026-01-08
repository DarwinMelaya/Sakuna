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
            
            // Ensure AudioManager doesn't have an AudioListener
            AudioListener audioListener = GetComponent<AudioListener>();
            if (audioListener != null)
            {
                Destroy(audioListener);
            }
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
        EnsureSingleAudioListener();
        PlaySceneMusic(scene.buildIndex);
    }

    void EnsureSingleAudioListener()
    {
        // Find all AudioListeners in the scene
        AudioListener[] listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
        Camera mainCamera = Camera.main;
        
        if (listeners.Length == 0)
        {
            // If no listeners exist, add one to the main camera
            if (mainCamera != null)
            {
                mainCamera.gameObject.AddComponent<AudioListener>();
            }
        }
        else if (listeners.Length > 1)
        {
            // Remove all listeners except the one on the main camera
            AudioListener listenerToKeep = null;
            
            // Prefer the listener on the main camera
            if (mainCamera != null)
            {
                listenerToKeep = mainCamera.GetComponent<AudioListener>();
            }
            
            // If main camera doesn't have one, keep the first listener that's not on AudioManager
            if (listenerToKeep == null)
            {
                foreach (AudioListener listener in listeners)
                {
                    if (listener.gameObject != gameObject)
                    {
                        listenerToKeep = listener;
                        break;
                    }
                }
            }
            
            // Remove all other listeners
            foreach (AudioListener listener in listeners)
            {
                if (listener != listenerToKeep)
                {
                    Destroy(listener);
                }
            }
            
            // Ensure main camera has a listener
            if (mainCamera != null && mainCamera.GetComponent<AudioListener>() == null)
            {
                mainCamera.gameObject.AddComponent<AudioListener>();
                // Remove the temporary listener if it wasn't on main camera
                if (listenerToKeep != null && listenerToKeep.gameObject != mainCamera.gameObject)
                {
                    Destroy(listenerToKeep);
                }
            }
        }
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
