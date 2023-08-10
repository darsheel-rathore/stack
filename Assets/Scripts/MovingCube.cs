using System;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }

    private void Awake()
    {
        if(gameObject.name == "Start" && LastCube == null)
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        else
            CurrentCube = this;
    }

    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * moveSpeed;
    }
    public void Stop()
    {
        moveSpeed = 0f;

        float hangover = transform.position.z - LastCube.transform.position.z;
        SplitCubeOnZ(hangover);
    }

    private void SplitCubeOnZ(float hangover)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = LastCube.transform.position.z + (hangover / 2);

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f;

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
    }
}
