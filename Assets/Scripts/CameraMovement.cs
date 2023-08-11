using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    public bool gameHasStarted = false;

    void Update()
    {
        if (!gameHasStarted)
            return;

        transform.position += Vector3.up * Time.deltaTime * moveSpeed;
    }
}
