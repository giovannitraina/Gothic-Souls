using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;
    private const int LeaderboardLength = 5;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SaveHighScore(string name, int score)
    {
        List<Scores> HighScores = new List<Scores>();

        int i = 1;
        while (i <= LeaderboardLength && PlayerPrefs.HasKey("HighScore" + i + "score"))
        {
            Scores temp = new Scores();
            temp.score = PlayerPrefs.GetInt("HighScore" + i + "score");
            temp.name = PlayerPrefs.GetString("HighScore" + i + "name");
            HighScores.Add(temp);
            i++;
        }
        if (HighScores.Count == 0)
        {
            Scores temp2 = new Scores();
            temp2.name = name;
            temp2.score = score;
            HighScores.Add(temp2);
        }
        else
        {
            for (i = 1; i <= HighScores.Count && i <= LeaderboardLength; i++)
            {
                if (score > HighScores[i - 1].score)
                {
                    Scores temp2 = new Scores();
                    temp2.name = name;
                    temp2.score = score;
                    HighScores.Insert(i - 1, temp2);
                    break;
                }
                if (i == HighScores.Count && i < LeaderboardLength)
                {
                    Scores temp2 = new Scores();
                    temp2.name = name;
                    temp2.score = score;
                    HighScores.Add(temp2);
                    break;
                }
            }
        }

        i = 1;
        while (i <= LeaderboardLength && i <= HighScores.Count)
        {
            PlayerPrefs.SetString("HighScore" + i + "name", HighScores[i - 1].name);
            PlayerPrefs.SetInt("HighScore" + i + "score", HighScores[i - 1].score);
            i++;
        }

    }

    public List<Scores> GetHighScore()
    {
        List<Scores> HighScores = new List<Scores>();

        int i = 1;
        while (i <= LeaderboardLength && PlayerPrefs.HasKey("HighScore" + i + "score"))
        {
            Scores temp = new Scores();
            temp.score = PlayerPrefs.GetInt("HighScore" + i + "score");
            temp.name = PlayerPrefs.GetString("HighScore" + i + "name");
            HighScores.Add(temp);
            i++;
        }

        return HighScores;
    }
}

public class Scores
{
    public int score;
    public string name;

}