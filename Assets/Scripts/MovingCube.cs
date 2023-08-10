using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    public static MovingCube CurrentCube { get; private set; }

    private void OnEnable()
    {
        CurrentCube = this;
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
    public void Stop()
    {
        moveSpeed = 0f;
    }
}
