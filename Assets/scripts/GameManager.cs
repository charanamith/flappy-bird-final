using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Start Screen")]
    public GameObject startPanel;

    [Header("Game Over Screen")]
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text bestScoreText;

    [Header("Horror Ambience")]
    public AudioSource horrorAmbience;

    private bool gameOver = false;

    void Awake()
    {
        Instance = this;

        Time.timeScale = 0f;

        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (horrorAmbience != null)
        {
            horrorAmbience.Stop();
        }
    }

    public void StartGame()
    {
        gameOver = false;

        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        Time.timeScale = 1f;

        if (horrorAmbience != null)
        {
            horrorAmbience.Play();
        }
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;

        int currentScore = 0;

        if (ScoreManager.Instance != null)
        {
            currentScore = ScoreManager.Instance.GetScore();
        }

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + currentScore;
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = "Best Score: " + bestScore;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (horrorAmbience != null)
        {
            horrorAmbience.Stop();
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}