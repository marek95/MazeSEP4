using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    private int pressedDir = NONE, oldPressedDir = NONE;

    private const int NONE = 0, FORWARD = 1, TURN_RIGHT = 2, TURN_LEFT = 3;

    private Vector3 moveTo;

    private Quaternion rotateTo;

    public bool canMove { set; get; }

    private Maze maze;

    private Vector2 winPosition;

    private Button btnForward, btnLeft, btnRight;

    void Awake()
    {
        btnForward = GameObject.Find("ForwardBtn").GetComponent<Button>();
        btnLeft = GameObject.Find("LeftBtn").GetComponent<Button>();
        btnRight = GameObject.Find("RightBtn").GetComponent<Button>();
    }

    void Start()
    {
        canMove = false;

        maze = GameObject.Find("Maze").GetComponent<Maze>();

        winPosition = new Vector2((int)maze.mazeSize / 2, 0);
    }

    void Update()
    {
        if (canMove)
        {

            if ((Input.GetKey(KeyCode.UpArrow) || btnForward.GetComponent<ButtonIsDown>().isPressed) && oldPressedDir != FORWARD)
                pressedDir = FORWARD;
            else if ((Input.GetKey(KeyCode.LeftArrow) || btnLeft.GetComponent<ButtonIsDown>().isPressed) && oldPressedDir != TURN_LEFT)
                pressedDir = TURN_LEFT;
            else if ((Input.GetKey(KeyCode.RightArrow) || btnRight.GetComponent<ButtonIsDown>().isPressed) && oldPressedDir != TURN_RIGHT)
                pressedDir = TURN_RIGHT;

            if (IsInGrid())
            {
                if (pressedDir == FORWARD)
                    MoveForward();

                else if (pressedDir == TURN_LEFT)
                    TurnLeft();

                else if (pressedDir == TURN_RIGHT)
                    TurnRight();

                oldPressedDir = pressedDir;
                pressedDir = NONE;

                if (transform.position.x == winPosition.x && transform.position.z == winPosition.y) {
                    GetComponent<WinLoseManager>().Win();
                    
                }
            }
            Move();
            Turn();
        }
    }

    public void MoveForward()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            Vector2Int positionForward = new Vector2Int((int)transform.position.x, (int)transform.position.z + 1);
            if (maze.GetPosition(positionForward) == MazeGenerator.CORRECT_WAY)
                moveTo = transform.position + Vector3.forward;
            else if (maze.GetPosition(positionForward) == MazeGenerator.CROSSROAD)
                GetComponent<WinLoseManager>().LostAttempt();
        }
        else if (transform.rotation.eulerAngles.y == 270f)
        {
            Vector2Int positionForward = new Vector2Int((int)transform.position.x - 1, (int)transform.position.z);
            if (maze.GetPosition(positionForward) == MazeGenerator.CORRECT_WAY)
                moveTo = transform.position + Vector3.left;
            else if (maze.GetPosition(positionForward) == MazeGenerator.CROSSROAD)
                GetComponent<WinLoseManager>().LostAttempt();
        }
        else if (transform.rotation.eulerAngles.y == 180f)
        {
            Vector2Int positionForward = new Vector2Int((int)transform.position.x, (int)transform.position.z - 1);
            if (maze.GetPosition(positionForward) == MazeGenerator.CORRECT_WAY)
                moveTo = transform.position + Vector3.back;
            else if (maze.GetPosition(positionForward) == MazeGenerator.CROSSROAD)
                GetComponent<WinLoseManager>().LostAttempt();
        }
        else if (transform.rotation.eulerAngles.y == 90f)
        {
            Vector2Int positionForward = new Vector2Int((int)transform.position.x + 1, (int)transform.position.z);
            if (maze.GetPosition(positionForward) == MazeGenerator.CORRECT_WAY)
                moveTo = transform.position + Vector3.right;
            else if (maze.GetPosition(positionForward) == MazeGenerator.CROSSROAD)
                GetComponent<WinLoseManager>().LostAttempt();
        }
    }

    public void TurnLeft()
    {
        if (transform.rotation.eulerAngles.y == 0)
            rotateTo = Quaternion.Euler(new Vector3(0, 270f, 0));
        else if (transform.rotation.eulerAngles.y == 270f)
            rotateTo = Quaternion.Euler(new Vector3(0, 180, 0));
        else if (transform.rotation.eulerAngles.y == 180f)
            rotateTo = Quaternion.Euler(new Vector3(0, 90f, 0));
        else if (transform.rotation.eulerAngles.y == 90f)
            rotateTo = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public void TurnRight()
    {
        if (transform.rotation.eulerAngles.y == 0)
            rotateTo = Quaternion.Euler(new Vector3(0, 90f, 0));
        else if (transform.rotation.eulerAngles.y == 270f)
            rotateTo = Quaternion.Euler(new Vector3(0, 0, 0));
        else if (transform.rotation.eulerAngles.y == 180f)
            rotateTo = Quaternion.Euler(new Vector3(0, 270f, 0));
        else if (transform.rotation.eulerAngles.y == 90f)
            rotateTo = Quaternion.Euler(new Vector3(0, 180f, 0));
    }

    private bool IsInGrid()
    {
        return transform.position.x % 1 == 0 &&
               transform.position.y % 1 == 0 &&
               transform.position.z % 1 == 0 &&
               transform.rotation.eulerAngles.y % 90 == 0;
    }
    private void Move()
    {
        float step = 3f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, moveTo, step);
    }

    private void Turn()
    {
        float step = 150f * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, step);
    }

    public IEnumerator LockMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(0.4f);
        canMove = true;
    }

    public void InitializePosition()
    {
        moveTo = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rotateTo = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        canMove = true;
    }
}
