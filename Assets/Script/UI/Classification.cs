using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Classification : MonoBehaviour
{
    public static Classification instance;

    [SerializeField] Text score1, score2, score3, total, namePlayer;
    private string player;
    private int totalCount = 0;
    [SerializeField] GameObject background;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void stampClassification()
    {
        Time.timeScale = 0;
        SoundManager.instance.Play("Victory", true);
        background.SetActive(true);
        score1.text = "LIVELLO1\n SCORE: " + (int)SaveGame.GetScore(1) + "\nTIME: " + (int)SaveGame.GetTime(1);
        score2.text = "LIVELLO2\n SCORE: " + (int)SaveGame.GetScore(2) + "\nTIME: " + (int)SaveGame.GetTime(2);
        score3.text = "LIVELLO3\n SCORE: " + (int)SaveGame.GetScore(3) + "\nTIME: " + (int)SaveGame.GetTime(3);
        totalCount = (int)SaveGame.GetScore(1) + (int)SaveGame.GetTime(1) + (int)SaveGame.GetScore(2) + (int)SaveGame.GetTime(2) + (int)SaveGame.GetScore(3) + (int)SaveGame.GetTime(3);
        total.text = "TOTAL: " + (int)totalCount;
    }

    public void saveInformation()
    {
        player = namePlayer.text;
        PlayerPrefs.SetString("PlayerName", player);
        PlayerPrefs.SetInt("TotalCount", totalCount);
        SceneManager.LoadScene("MainMenu");
    }


}
