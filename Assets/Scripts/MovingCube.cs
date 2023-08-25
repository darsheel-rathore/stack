using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    // Properties to keep track of the current and last cubes
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; set; }

    // Property to specify the cube's movement direction
    public MoveDirection MoveDirection { get; internal set; }

    private void OnEnable()
    {
        // Initialize the last cube or current cube based on cube name
        if (gameObject.name == "Start" && LastCube == null)
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        else
            CurrentCube = this;

        // Set a random color for the cube
        GetComponent<Renderer>().material.color = GetRandomColor();

        // Adjust the cube's size based on the last cube's size
        transform.localScale = new Vector3(LastCube.transform.localScale.x,
            transform.localScale.y,
            LastCube.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        // Generate a random color
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    void Update()
    {
        // Move the cube based on its movement direction
        if (MoveDirection == MoveDirection.Z)
        {
            transform.position -= transform.forward * Time.deltaTime * moveSpeed;
        }
        else
        {
            transform.position -= transform.right * Time.deltaTime * moveSpeed;
        }
    }

    // Function to stop the cube's movement
    public void Stop()
    {
        // Stop the cube's movement
        moveSpeed = 0f;

        // Calculate the overhang
        float hangover = GetHangOver();

        // Handle the case based on the movement direction
        if (MoveDirection == MoveDirection.Z)
            hangover = transform.position.z - LastCube.transform.position.z;
        else
            hangover = transform.position.x - LastCube.transform.position.x;

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;

        // Check if the overhang is too large, indicating a game over
        if (Mathf.Abs(hangover) >= max)
        {
            LastCube = null;
            CurrentCube = null;
        }

        float direction = hangover > 0 ? 1f : -1f;

        if (LastCube != null)
        {
            // Split the cube based on the overhang
            if (MoveDirection == MoveDirection.Z)
                SplitCubeOnZ(hangover, direction);
            else
                SplitCubeOnX(hangover, direction);

            // Calculate the score
            GameManager.instance.ScoreCalculator();
            // Invoke the score event
            GameManager.instance.RaiseTheScoreCalculationEvent();
        }
    }

    // Function to split the cube based on the Z-axis overhang
    private void SplitCubeOnZ(float hangover, float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = LastCube.transform.position.z + (hangover / 2);

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        // Spawn a cube that falls off
        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        // Calculate the new size for the cube along the X-axis
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        // Calculate the size of the falling block
        float fallingBlockSize = transform.localScale.x - newXSize;

        // Calculate the new X position for the current cube
        float newXPosition = LastCube.transform.position.x + (hangover / 2);

        // Update the scale and position of the current cube
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        // Calculate the edge of the cube based on the new size and direction
        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        // Calculate the Z position for the falling block based on the cube edge and size
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        // Spawn a cube that falls off (falling block)
        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    // Function to spawn a cube that falls off (falling block)
    private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
    {
        // Create a new cube for the falling block
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (MoveDirection == MoveDirection.Z)
        {
            // Set the scale and position for the falling block in the Z direction
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockPosition);
        }
        else
        {
            // Set the scale and position for the falling block in the X direction
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockPosition, transform.position.y, transform.position.z);
        }

        // Add a Rigidbody component to enable physics simulation
        cube.AddComponent<Rigidbody>();
        // Set the color of the falling block to match the color of the current cube
        cube.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
        // Destroy the falling block after a delay (cleanup)
        Destroy(cube.gameObject, 1f);

        // Update the reference to the last cube
        UpdateLastCubeRef();
    }

    // Function to update the reference to the last cube (current cube becomes last cube)
    public void UpdateLastCubeRef()
    {
        LastCube = this;
    }

    // Function to calculate the overhang based on the movement direction
    public float GetHangOver()
    {
        if (LastCube == null)
            return 0;

        if (MoveDirection == MoveDirection.Z)
            return transform.position.z - LastCube.transform.position.z;
        else
            return transform.position.x - LastCube.transform.position.x;
    }
}
