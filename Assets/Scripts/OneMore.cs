using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneMore : MonoBehaviour
{
    //Реализация кнопки "Еще раз"
    public GameObject OneMoreWindow;

    public void OneMoreGame()
    {
        foreach(var title in FindObjectsOfType<Title>())
        {
            title.ReturnToDefaultPosition();
        }
        OneMoreWindow.SetActive(false);
    }
}
