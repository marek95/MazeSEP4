using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    GameObject gameUI;

    void Start()
    {
        gameUI = GameObject.Find("GameUI");
        Hide();
    }

    public void Show()
    {
        gameUI.SetActive(true);
    }

    public void Hide()
    {
        gameUI.SetActive(false);
    }
}
