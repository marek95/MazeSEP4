using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnSelectDifficultyClick(int difficulty)
    {
        GameManager.difficulty = difficulty;
        SceneManager.LoadScene("Game");
    }
}
