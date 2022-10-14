using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Game : MonoBehaviour
{
    
    public static Game instance { get; private set; } //Синглтон

    
    public GameObject Menu, Level, Settings, FailLabel, WinLabel, OneMoreWindow; //Окна, которые будут открываться
    
    public Transform TargetCeil; //Ячейка, в которую нужно будет вставить кубик
    public SpriteRenderer[] TitleSprites; //Анимация исчезновения будет в коде выигрыша. Уместнее поместить это в класс Title, чтобы создать полноценную игру
    void Awake()
    {
        instance = this;
    }
    
    public void Play()
    {
        Menu.SetActive(false);
        Level.SetActive(true);
    }

    IEnumerator ActivateWin()
    {
        float value = 1;
        do
        {
            yield return new WaitForSeconds(0.02f);
            foreach(var item in TitleSprites)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, value);
            }
            value -= 0.02f;
        } while (value >= 0);
        OneMoreWindow.SetActive(true);
        WinLabel.SetActive(true);
        FailLabel.SetActive(false);
    }
    void ActivateFail()
    {
        OneMoreWindow.SetActive(true);
        WinLabel.SetActive(false);
        FailLabel.SetActive(true);
    }
    public void ActivateSettings(bool active)
    {
        Settings.SetActive(active);
    }
    public void CheckResult()
    {
        if (InARow())
        {
            StartCoroutine(ActivateWin());
        }
        else
        {
            ActivateFail();
        }
    }

    bool InARow() //Я так понял, получается игра типа "Три в ряд". Но я не стал допиливать ее в коде именно под механику "Три в ряд", а просто проверил, лежит ли кубик в нужной ячейке или нет
    {
        return TargetCeil.childCount != 0;
    }
}
