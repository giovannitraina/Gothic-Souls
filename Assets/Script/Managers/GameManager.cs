using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject tutorial;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        if(PlayerPrefs.GetInt("Tutorial") == 1)
        {
            tutorial.SetActive(true);
        }
        PlayerPrefs.DeleteKey("Tutorial");
        
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello1"))
        {
            SaveGame.SaveScene(1);
            SoundManager.instance.Play("MainTheme1", true);
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello2"))
        {
            SaveGame.SaveScene(2);
            SoundManager.instance.Play("MainTheme2", true);
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello3"))
        {
            SaveGame.SaveScene(3);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        SoundManager.instance.Stop("MainTheme1");
        SoundManager.instance.Stop("MainTheme2");
        SoundManager.instance.Stop("BossBattle");
        SoundManager.instance.Play("GameOver");
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        scoreText.text = "SCORE: "+ ScoreManager.instance.score.ToString();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
