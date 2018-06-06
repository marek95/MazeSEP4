using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinLoseManager : MonoBehaviour
{
    GameObject gameUI;

    Image firstLife, secondLife;
    public int attemptsLeft;

    public GameObject winInfo, loseInfo;

    void Awake()
    {
        gameUI = GameObject.Find("GameUI");
        firstLife = GameObject.Find("FirstLife").GetComponent<Image>();
        secondLife = GameObject.Find("SecondLife").GetComponent<Image>();
        attemptsLeft = 3;
    }

    public void LostAttempt()
    {
        CameraShake.Shake();
        attemptsLeft--;
        if (attemptsLeft == 2)
        {
            StartCoroutine(GetComponent<FirstPersonMovement>().LockMovement());

            Color fadeColor = firstLife.color;
            fadeColor.a = 0.4f;
            firstLife.color = fadeColor;
        }
        else if (attemptsLeft == 1)
        {
            StartCoroutine(GetComponent<FirstPersonMovement>().LockMovement());

            Color fadeColor = secondLife.color;
            fadeColor.a = 0.4f;
            secondLife.color = fadeColor;
        }
        else if (attemptsLeft <= 0)
        {
            gameUI.GetComponent<GameUI>().Hide();
            GetComponent<FirstPersonMovement>().canMove = false;
            StartCoroutine(GetComponent<CameraController>().MoveCameraUp());
            StartCoroutine(LoseCoroutine());
        }
    }

    public void Win()
    {
        gameUI.GetComponent<GameUI>().Hide();
        GetComponent<FirstPersonMovement>().canMove = false;
        StartCoroutine(GetComponent<CameraController>().MoveCameraUp());
        StartCoroutine(WinCoroutine());
    }

    IEnumerator WinCoroutine()
    {
        yield return new WaitForSeconds(1);
        winInfo.GetComponent<WinLoseUI>().Show();
        yield return new WaitForSeconds(3);

        winInfo.GetComponent<WinLoseUI>().hide();

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator LoseCoroutine()
    {
        yield return new WaitForSeconds(1);
        loseInfo.GetComponent<WinLoseUI>().Show();
        yield return new WaitForSeconds(3);

        loseInfo.GetComponent<WinLoseUI>().hide();

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
}