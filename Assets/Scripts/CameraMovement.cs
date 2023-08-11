using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f; // Speed at which the camera moves
    public bool gameHasStarted = false; // Indicates if the game has started

    void Update()
    {
        if (!gameHasStarted)
            return; // If the game hasn't started, don't move the camera

        // Move the camera upward based on the moveSpeed and frame rate
        transform.position += Vector3.up * Time.deltaTime * moveSpeed;
    }
}
