using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string mainGameSceneName = "GameView";
    [SerializeField] private string mainMenuSceneName = "StartMenu"; // Replace with your exact start scene name

    // Called by Start Button in Main Menu
    public void OnStartClick()
    {
        Debug.Log("[Menu] Loading main game scene...");
        SceneManager.LoadScene(mainGameSceneName);
    }

    // Called by Back Button in Game View
    public void OnBackClick()
    {
        Debug.Log("[Menu] Returning to main menu...");
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // Called by Exit Button
    public void OnExitClick()
    {
        Debug.Log("[Menu] Exiting application...");

        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}