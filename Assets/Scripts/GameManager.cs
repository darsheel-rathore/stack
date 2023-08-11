using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CubeSpawner[] spawners; // Array of CubeSpawner objects in the scene
    private CubeSpawner currentSpawner; // The current cube spawner
    private int spawnerIndex; // Index of the current spawner
    [SerializeField] private ScoreEvent scoreEvent; // Event to handle scoring
    private int score = 0; // The current score

    private void Awake()
    {
        // Find all CubeSpawner objects in the scene
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Check for left mouse button click
        {
            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop(); // Stop the current moving cube

            // Start camera upward movement
            Camera.main.GetComponent<CameraMovement>().gameHasStarted = true;

            // Toggle between two spawners
            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex]; // Get the current spawner

            currentSpawner.SpawnCube(); // Spawn a cube from the current spawner

            // Calculate the score
            ScoreCalculator();

            // Invoke the score event
            scoreEvent.RaiseEvent(this, score);
        }
    }

    // Function to calculate the score
    public void ScoreCalculator()
    {
        score++; // Increment the score
    }
}

