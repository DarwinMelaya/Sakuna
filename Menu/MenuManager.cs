using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // This function loads Scene1 when Play button is clicked
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Loads scene at index 1 (Scene1)
    }

    // This function quits the game when Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quitting game..."); // Shows in Unity Editor
        Application.Quit(); // Quits the application
        
        // Note: Application.Quit() only works in builds, not in Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in editor
        #endif
    }
}