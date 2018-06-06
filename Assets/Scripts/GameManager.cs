using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int difficulty { set; get; }

    void Start()
    {
        difficulty = 1;
    }
}
