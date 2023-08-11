using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component for displaying the score

    // Function to handle updates to the score
    public void OnScoreUpdate(Component sender, object data)
    {
        // Format the new text with the updated score and display it
        string newText = "Score : " + data.ToString();
        scoreText.text = newText;
    }
}
