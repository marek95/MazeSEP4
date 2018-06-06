using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    GameObject playerObject, maze, GameUI;

    void Start()
    {
        playerObject = GameObject.Find("Player");
        maze = GameObject.Find("Maze");
        GameUI = GameObject.Find("GameUI");

        StartCoroutine(PreviewMazeCoroutine());
    }

    IEnumerator PreviewMazeCoroutine()
    {
        int size = maze.GetComponent<Maze>().GetMazeSize();
        playerObject.GetComponent<CameraController>().PutAbove(size);

        yield return new WaitForSeconds(Timer.waitTime);    // Wait

        // hide correct path
        GameObject[] correctWayObjects = GameObject.FindGameObjectsWithTag("CorrectWay");
        foreach (var correctWayObject in correctWayObjects)
        {
            correctWayObject.SetActive(false);
        }

        Vector2 startingPosition = maze.GetComponent<Maze>().GetStartPosition();
        yield return StartCoroutine(playerObject.GetComponent<CameraController>().MoveCameraToStartCoroutine(3, startingPosition));

        // initialize 
        playerObject.GetComponent<FirstPersonMovement>().InitializePosition();

        GameUI.GetComponent<GameUI>().Show();
    }
}
