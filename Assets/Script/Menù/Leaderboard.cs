using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] GameObject[] positions;
    public List<Scores> highscores;

    private void Start()
    {
        highscores = HighScoreManager.instance.GetHighScore();
        for(int i=0; i < highscores.Count; i++)
        {
                positions[i].GetComponentsInChildren<Text>()[1].text = highscores[i].name.ToUpper();
                positions[i].GetComponentsInChildren<Text>()[2].text = highscores[i].score.ToString();
        }
    }
}
