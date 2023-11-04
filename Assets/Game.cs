using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Game : MonoBehaviour
{
    // Every time the game restarts, the points are 0
    public int points = 0;
 
    // This is connected to the Text in our Canvas > Panel GameObject
     public TextMeshProUGUI score;

    // Call this method from PlayerController when they collect a point
    public void AddPoint()
    {
        points++;
        score.text = $"Points: {points}";
    }
}
