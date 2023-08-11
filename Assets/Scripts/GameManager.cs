using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CubeSpawner[] spawners;
    private CubeSpawner currentSpawner;
    private int spawnerIndex;

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            // Start camera upside movement
            Camera.main.GetComponent<CameraMovement>().gameHasStarted = true;

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;

            currentSpawner = spawners[spawnerIndex];

            currentSpawner.SpawnCube();
        }
    }
}
