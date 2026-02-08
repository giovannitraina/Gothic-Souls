using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] Text portalUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerItemCollider"))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello1"))
            {
                if (CoinManager.instance.coin != 35)
                {
                    portalUI.gameObject.SetActive(true);
                }
                else
                {
                    portalUI.gameObject.SetActive(false);
                    ScoreManager.instance.SaveScore(1);
                    Timer.instance.SaveTime(1);
                    SaveGame.SaveScene(2);
                    SceneManager.LoadScene("Livello2");
                }
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello2"))
            {
                if (CoinManager.instance.coin != 35)
                {
                    portalUI.gameObject.SetActive(true);
                }
                else
                {
                    portalUI.gameObject.SetActive(false);
                    ScoreManager.instance.SaveScore(2);
                    Timer.instance.SaveTime(2);
                    SaveGame.SaveScene(3);
                    SaveGame.SaveStats();
                    SceneManager.LoadScene("Livello3");
                }
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello3"))
            {
                if (CoinManager.instance.coin != 1)
                {
                    portalUI.gameObject.SetActive(true);
                }
                else
                {
                    portalUI.gameObject.SetActive(false);
                    ScoreManager.instance.SaveScore(3);
                    Timer.instance.SaveTime(3);
                    SaveGame.SaveScene(1);
                    Classification.instance.stampClassification();
                }
            }

        }
    }
}
