using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Level1Inro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject playButton;

    void Start()
    {
        playButton.SetActive(false);
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        playButton.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2); // GameScene
    }
}
