using UnityEngine;
using UnityEngine.SceneManagement; // Required to use SceneManager

public class StartMenuController : MonoBehaviour
{
    // Match this exact string to your actual game scene name in Assets/Scenes
    [SerializeField] private string mainGameSceneName = "GameView";

    public void OnStartClick()
    {
        Debug.Log("[StartMenu] Loading main game scene...");
        SceneManager.LoadScene(mainGameSceneName);
    }

    public void OnExitClick()
    {
        Debug.Log("[StartMenu] Exiting application...");

        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
