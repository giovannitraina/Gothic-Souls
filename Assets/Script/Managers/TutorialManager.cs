using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] popUps;
    private int popUpIndex;
    public static TutorialManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = 0;
        popUpIndex = PlayerPrefs.GetInt("Controlls");
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            popUps[popUpIndex].SetActive(false);
            if (popUpIndex == 0)
            {
                popUpIndex++; //Per evitare il doppio popup sui movimenti
            }
            popUpIndex++;
        }

        for(int i=0; i < popUps.Length; i++)
        {   
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
        }

        if (popUpIndex == popUps.Length)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
