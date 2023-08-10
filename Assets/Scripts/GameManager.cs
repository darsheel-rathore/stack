using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            FindAnyObjectByType<CubeSpawner>().SpawnCube();

            // Start camera upside movement
            Camera.main.GetComponent<CameraMovement>().gameHasStarted = true;
        }
    }
}
