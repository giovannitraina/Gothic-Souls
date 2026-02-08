using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public bool tutorial;
    [SerializeField] GameObject noSave;

    public void New()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
        for(int i=1; i < 4; i++)
        {
            PlayerPrefs.SetFloat("Score" + i, 0);
            PlayerPrefs.SetFloat("Time" + i, 0);
        }
        PlayerPrefs.DeleteKey("MaxHp");
        PlayerPrefs.DeleteKey("AttackDmg");
        SceneManager.LoadScene("Livello1");
    }

    public void Load()
    {
        switch (SaveGame.GetScene())
        {
            case 1:
                noSave.SetActive(true);
                break;
            case 2:
                SceneManager.LoadScene("Livello2");
                break;
            case 3:
                SceneManager.LoadScene("Livello3");
                break;
            default:
                noSave.SetActive(true);
                break;
        }
    }
}
