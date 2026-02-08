using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    [SerializeField] TextMesh text;
    public int coin = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello3"))
        {
            text.text = coin.ToString() + "/1";
        }
        else
        {
            text.text = coin.ToString() + "/35";
        }
    }
    public void AddCoin(int coinvalue)
    {
        coin += coinvalue;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello3"))
        {
            text.text = coin.ToString() + "/1";
        }
        else
        {
            text.text = coin.ToString() + "/35";
        }
    }
}
