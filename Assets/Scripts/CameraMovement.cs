using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float moveSpeed = 0.065f; // Speed at which the camera moves 0.075f

    void Update()
    {
        if (!GameManager.isGameStarted)
            return; // If the game hasn't started, don't move the camera

        // Move the camera upward based on the moveSpeed and frame rate
        transform.position += Vector3.up * Time.deltaTime * moveSpeed;
    }
}
