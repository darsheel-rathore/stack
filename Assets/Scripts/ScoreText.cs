using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    
    public void OnScoreUpdate(Component sender, object data)
    {
        string newText = "Score : " + data.ToString();
        scoreText.text = newText;
    }
}
