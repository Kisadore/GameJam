using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject howToPlayPanel;
    
    private AudioManager audioManager;
    
    void Start()
    {
        // Get AudioManager reference
        audioManager = AudioManager.Instance;
        
        // Play main menu music
        if (audioManager != null)
        {
            audioManager.PlayMainMenuMusic();
        }
        
        // Show main panel, hide others
        ShowMainMenu();
    }
    
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        
        // Play button click sound
        if (audioManager != null)
        {
            audioManager.PlayButtonClick();
        }
    }
    
    public void ShowHowToPlay()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        
        // Play button click sound
        if (audioManager != null)
        {
            audioManager.PlayButtonClick();
        }
    }
    
    public void StartGame()
    {
        // Play button click sound
        if (audioManager != null)
        {
            audioManager.PlayButtonClick();
        }
        
        SceneManager.LoadScene("GameScene");
    }
    
    public void QuitGame()
    {
        // Play button click sound
        if (audioManager != null)
        {
            audioManager.PlayButtonClick();
        }
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}