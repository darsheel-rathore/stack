using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private MovingCube cubePrefab; // Prefab for the moving cube to be spawned
    [SerializeField] private MoveDirection moveDirection; // The direction in which the cube will move

    // Function to spawn a cube
    public void SpawnCube()
    {
        var cube = Instantiate(cubePrefab);

        // Check if there is a previous cube and it's not the "Start" cube
        if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject.name != "Start")
        {
            // Determine the new cube's position based on the move direction
            float x = moveDirection == MoveDirection.X ? transform.position.x : MovingCube.LastCube.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : MovingCube.LastCube.transform.position.z;

            // Set the new cube's position above the last cube
            cube.transform.position = new Vector3(
                x, MovingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y, z);
        }
        else
        {
            // If this is the first cube or the last cube was the "Start" cube, place the new cube at the spawner's position
            cube.transform.position = transform.position;
        }

        // Set the move direction for the new cube
        cube.MoveDirection = moveDirection;
    }

    // Draw a wire cube gizmo in the Unity editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // Draw a wire cube at the spawner's position with the size of the cubePrefab
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}

