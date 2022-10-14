using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Music : MonoBehaviour
{
    // Настройки музыки
    public AudioSource AS;
    float Volume;
    public Slider slider;
    public TextMeshProUGUI Label;
    void Start()
    {
        Volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        AS.volume = Volume;
        slider.value = Volume;
        Label.text = "ЗВУК: " + (int)(Volume * 100) + "%";
    }

    public void SetVolume()
    {
        Volume = slider.value;
        PlayerPrefs.SetFloat("MusicVolume", Volume);
        AS.volume = Volume;
        Label.text = "ЗВУК: " + (int)(Volume * 100) + "%";
    }
}
