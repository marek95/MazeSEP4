using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 moveTo;
    private Quaternion rotateTo;

    public void PutAbove(int size)
    {
        // put camera above maze
        transform.position = new Vector3(size / 2, size+2, size / 2);
        transform.rotation = Quaternion.Euler(new Vector3(90, 180, 0));
    }

    public IEnumerator MoveCameraUp()
    {
        float duration = 2f;
        float endTime = Time.time + duration;
        float elapsedTime = 0;

        moveTo = new Vector3(transform.position.x, 3, transform.position.z);
        rotateTo = Quaternion.Euler(new Vector3(270, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));

        Vector3 upPosition = transform.position;
        Quaternion upRotation = transform.rotation;

        while (Time.time < endTime)
        {
            transform.position = Vector3.Lerp(upPosition, moveTo, elapsedTime / duration);
            transform.rotation = Quaternion.Lerp(upRotation, rotateTo, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator MoveCameraToStartCoroutine(float duration, Vector2 startingPosition)
    {
        float endTime = Time.time + duration;
        float elapsedTime = 0;
        moveTo = new Vector3(startingPosition.x, 1, startingPosition.y);
        rotateTo = Quaternion.Euler(new Vector3(0, 180, 0));
        Vector3 upPosition = transform.position;
        Quaternion upRotation = transform.rotation;

        while (Time.time < endTime)
        {
            transform.position = Vector3.Lerp(upPosition, moveTo, elapsedTime / duration);
            transform.rotation = Quaternion.Lerp(upRotation, rotateTo, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
