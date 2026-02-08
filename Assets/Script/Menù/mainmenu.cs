using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    private string player;
    private int totalCount;

    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Resolution"))
        {
            Screen.SetResolution(800, 600, false);
        }

        SoundManager.instance.Play("Menu", true);
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            player = PlayerPrefs.GetString("PlayerName");
            totalCount = PlayerPrefs.GetInt("TotalCount");

            PlayerPrefs.DeleteKey("PlayerName");
            PlayerPrefs.DeleteKey("TotalCount");

            HighScoreManager.instance.SaveHighScore(player, totalCount);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
