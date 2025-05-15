using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform[] platforms;
    public GameObject cardPrefab;
    public TMP_Text scoreText;
    public float cardRevealSpeed = 2f;
    
    [Header("UI Panels")]
    public GameObject pausePanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public TMP_Text loseScoreText;
    public TMP_Text winScoreText;
    
    [Header("Game Settings")]
    public int scoreToWin = 10;
    
    private PlayerController player;
    private int score = 0;
    private GameObject currentCard;
    private bool roundInProgress = false;
    private bool gameOver = false;
    private AudioManager audioManager;
    
    void Start()
    {
        // Get AudioManager reference
        audioManager = AudioManager.Instance;
        
        // Play gameplay music
        if (audioManager != null)
        {
            audioManager.PlayGameplayMusic();
        }
        
        // Hide all panels at start
        pausePanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        
        player = FindObjectOfType<PlayerController>();
        player.SetPlatforms(platforms);
        player.OnPlayerMoved += HandlePlayerMoved;
        UpdateScoreUI();
    }
    
    void Update()
    {
        if (!gameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    public void TogglePause()
    {
        bool isPaused = pausePanel.activeSelf;
        pausePanel.SetActive(!isPaused);
        
        // Play button sound
        if (audioManager != null)
        {
            audioManager.PlayButtonClick();
        }
        
        if (isPaused)
        {
            // Resume game
            Time.timeScale = 1f;
            
            if (audioManager != null)
            {
                audioManager.ResumeMusic();
            }
        }
        else
        {
            // Pause game
            Time.timeScale = 0f;
            
            if (audioManager != null)
            {
                audioManager.PauseMusic();
            }
        }
    }
    
    void HandlePlayerMoved(int platformIndex)
    {
        if (roundInProgress || gameOver) return;
        
        roundInProgress = true;
        
        // Play player move sound
        if (audioManager != null)
        {
            audioManager.PlayPlayerMoveSound();
        }

        int cardPlatformIndex = Random.Range(0, platforms.Length);
        
        Vector3 cardPosition = platforms[cardPlatformIndex].position;
        cardPosition.y += 0.69f; 
        currentCard = Instantiate(cardPrefab, cardPosition, Quaternion.Euler(0, -90, 0));

        currentCard.transform.localScale = Vector3.zero;

        StartCoroutine(RevealCard(cardPlatformIndex, platformIndex));
    }
    
    IEnumerator RevealCard(int cardPlatformIndex, int playerPlatformIndex)
    {
        // Play card appear sound
        if (audioManager != null)
        {
            audioManager.PlayCardAppearSound();
        }

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * cardRevealSpeed;
            currentCard.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time);
            yield return null;
        }
        
        if (cardPlatformIndex == playerPlatformIndex)
        {
            // Game over - player loses
            yield return new WaitForSeconds(1f);
            ShowLoseScreen();
        }
        else
        {
            // Success - increment score
            score++;
            UpdateScoreUI();
            
            // Play score point sound
            if (audioManager != null)
            {
                audioManager.PlayScorePointSound();
            }
            
            if (score >= scoreToWin)
            {
                yield return new WaitForSeconds(1f);
                ShowWinScreen();
            }
            else
            {
                yield return new WaitForSeconds(1f);
                Destroy(currentCard);
                roundInProgress = false;
            }
        }
    }
    
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
    
    void ShowLoseScreen()
    {
        gameOver = true;
        loseScoreText.text = "Final Score: " + score;
        losePanel.SetActive(true);
        
        // Play lose sound
        if (audioManager != null)
        {
            audioManager.PlayLoseSound();
        }
    }
    
    void ShowWinScreen()
    {
        gameOver = true;
        winScoreText.text = "Final Score: " + score;
        winPanel.SetActive(true);
        
        // Play win sound
        if (audioManager != null)
        {
            audioManager.PlayWinSound();
        }
    }
    
    public void RestartGame()
    {
        // Play button click sound
        if (audioManager != null)
        {
            audioManager.PlayButtonClick();
        }
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitToMainMenu()
    {
        // Play button click sound
        if (audioManager != null)
        {
            audioManager.PlayButtonClick();
        }
        
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ResumeGame()
    {
        // Play button click sound
        if (audioManager != null)
        {
            audioManager.PlayButtonClick();
        }
        
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        
        if (audioManager != null)
        {
            audioManager.ResumeMusic();
        }
    }
}