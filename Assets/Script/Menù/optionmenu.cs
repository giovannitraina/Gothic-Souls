using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class optionmenu : MonoBehaviour
{
    [SerializeField] Slider sliderSound;
    [SerializeField] Slider sliderMusic;
    [SerializeField] Toggle fullscreen;
    [SerializeField] Dropdown dropdownResolution;
    [SerializeField] Dropdown dropdownControlls;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Sound") || PlayerPrefs.HasKey("Music"))
        {
            sliderSound.value = PlayerPrefs.GetFloat("Sound");
            sliderMusic.value = PlayerPrefs.GetFloat("Music");
        }
        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            fullscreen.isOn = true;
        }
        else
        {
            fullscreen.isOn = false;
        }

        dropdownResolution.value = PlayerPrefs.GetInt("Resolution");
        dropdownControlls.value = PlayerPrefs.GetInt("Controlls");
    }

    public void setSound(float slider)
    {
        PlayerPrefs.SetFloat("Sound", slider);
    }
    public void setMusic(float slider)
    {
        PlayerPrefs.SetFloat("Music", slider);
        SoundManager.instance.ChangeVolume("Menu", slider);
    }

    public void Setfullscreen(bool full)
    {
        Screen.fullScreen = full;
        if (full)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
    }

    public void setControlls(int index)
    {
        switch (index)
        {
            case 0:
                PlayerPrefs.SetInt("Controlls", 0);
                break;
            case 1:
                PlayerPrefs.SetInt("Controlls", 1);
                break;
        }
    }

    public void setResolution(int index)
    {
        switch (index)
        {
            case 0:
                if(PlayerPrefs.GetInt("Fullscreen") == 1)
                    Screen.SetResolution(800, 600, true);
                else
                    Screen.SetResolution(800, 600, false);
                break;
            case 1:
                if (PlayerPrefs.GetInt("Fullscreen") == 1)
                    Screen.SetResolution(1280, 720, true);
                else
                    Screen.SetResolution(1280, 720, false);
                break;
            case 2:
                if (PlayerPrefs.GetInt("Fullscreen") == 1)
                    Screen.SetResolution(1920, 1080, true);
                else
                    Screen.SetResolution(1920, 1080, false);
                break;
        }

        PlayerPrefs.SetInt("Resolution", index);
    }
}
