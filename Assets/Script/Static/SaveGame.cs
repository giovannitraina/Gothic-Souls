using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public static void SaveScore(float score, int i)
    {
        PlayerPrefs.SetFloat("Score"+i, score);
        PlayerPrefs.Save();
    }

    public static void SaveTime(float time, int i)
    {
        PlayerPrefs.SetFloat("Time" + i, time);
        PlayerPrefs.Save();
    }

    public static float GetTime(int i)
    {
        return PlayerPrefs.GetFloat("Time" + i);
    }


    public static float GetScore(int i)
    {
        return PlayerPrefs.GetFloat("Score"+i);
    }

    public static void SaveScene(int scene)
    {
        PlayerPrefs.SetInt("Scene", scene);
        PlayerPrefs.Save();
    }

    public static int GetScene()
    {
        return PlayerPrefs.GetInt("Scene");
    }

    public static void SaveStats()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PlayerPrefs.SetFloat("MaxHp", player.GetMaxHp());
        PlayerPrefs.SetFloat("AttackDmg", player.GetAttackDamage());
        PlayerPrefs.Save();
    }
}
