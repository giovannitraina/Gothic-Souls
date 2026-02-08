using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float currentTime;
    [SerializeField] float startingTime;
    private TextMesh counter;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentTime = 0f;
        counter = GetComponent<TextMesh>();
    }

    private void Start()
    {
        currentTime = startingTime;
    }

    void FixedUpdate()
    {
        currentTime -= 1 * Time.deltaTime;
        counter.text = currentTime.ToString("0");
  
        if(currentTime<=0)
         {
            GameManager.instance.GameOver();
         }
    }

    public void SaveTime(int indice)
    {
        SaveGame.SaveTime(currentTime, indice);
    }
}
