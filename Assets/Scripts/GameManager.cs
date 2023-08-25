using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private CubeSpawner[] spawners; // Array of CubeSpawner objects in the scene
    private CameraMovement cam;
    private CubeSpawner currentSpawner; // The current cube spawner
    private int spawnerIndex; // Index of the current spawner
    [SerializeField] private ScoreEvent scoreEvent; // Event to handle scoring
    
    private int currentScore = 0; // The current score
    private int highScore = 0; // The high score

    public static bool isGameStarted = false;
    public static bool isCollidedWithDestroyer = false;

    public GameObject welcomeMsgText;
    public GameObject clickToPlayText;
    public GameObject scoreText;
    public GameObject highScoreText;
    public GameObject gameOverText;
    public GameObject returnToHomeText;

    private void Awake()
    {
        instance = this;

        // Find all CubeSpawner objects in the scene
        spawners = FindObjectsOfType<CubeSpawner>();
        cam = Camera.main.GetComponent<CameraMovement>();

        // Load the high score from PlayerPrefs only if it exists
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", 0);
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    private void OnEnable()
    {
        isGameStarted = false;
        isCollidedWithDestroyer = false;
    }

    private void Start()
    {
        // Set the high score
        highScoreText.GetComponent<TextMeshProUGUI>().text = $"High Score: {highScore}";

        DisableUI();
        welcomeMsgText.SetActive(true);
        highScoreText.SetActive(true);
        clickToPlayText.SetActive(true);

    }

    private void Update()
    {
        // Load the scene at game over when user press down the mouse button
        if (MovingCube.LastCube == null && Input.GetKeyDown(KeyCode.Mouse0) && !isGameStarted)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) // Check for left mouse button click
        {
            if (MovingCube.CurrentCube != null && MovingCube.LastCube != null)
                MovingCube.CurrentCube.Stop(); // Stop the current moving cube

            // Start the game
            isGameStarted = true;

            // Show the UI
            DisableUI();
            scoreText.SetActive(true);

            // Toggle between two spawners
            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex]; // Get the current spawner

            if(MovingCube.LastCube != null)
                currentSpawner.SpawnCube(); // Spawn a cube from the current spawner

        }

        // Game Over calculations
        if (MovingCube.LastCube == null)
        {
            if (isGameStarted)
            {
                isGameStarted = false;

                // Show the Game over UI
                DisableUI();
                highScoreText.GetComponent<TextMeshProUGUI>().text = $"High Score: {highScore}";
                gameOverText.SetActive(true);
                scoreText.SetActive(true);
                highScoreText.SetActive(true);
                returnToHomeText.SetActive(true);

                // Let the cube fall to the ground
                if (!isCollidedWithDestroyer)
                {
                    GameObject[] movingCubes = GameObject.FindGameObjectsWithTag("MovingCube");
                    var lastCube = movingCubes[movingCubes.Length - 1];
                    lastCube.AddComponent<Rigidbody>();
                    Destroy(lastCube.gameObject, 3f);
                }
            }
        }
    }

    // Function to calculate the score
    public void ScoreCalculator()
    {
        currentScore++; // Increment the score

        // Check if the current score is higher than the high score
        if (currentScore > highScore)
        {
            // Update the high score
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    // Raise Score event
    public void RaiseTheScoreCalculationEvent()
    {
        scoreEvent.RaiseEvent(this, currentScore);
    }

    // Disable all the UI
    private void DisableUI()
    {
        welcomeMsgText.SetActive(false);
        clickToPlayText.SetActive(false);
        scoreText.SetActive(false);
        highScoreText.SetActive(false);
        gameOverText.SetActive(false);
        returnToHomeText.SetActive(false);
    }
}

