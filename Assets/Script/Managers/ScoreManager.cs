using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public float score = 0;
    [SerializeField] TextMesh text;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        text.text = score.ToString();
    }
    public void ChangeScore(float points)
    {
        score += points;
        text.text = score.ToString();
    }

    public void SaveScore(int indice)
    {
        SaveGame.SaveScore(score, indice);
    }
}
