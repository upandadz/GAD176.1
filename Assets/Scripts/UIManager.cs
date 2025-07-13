using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text playerXWonText;
    [SerializeField] private TMP_Text scoredXPoints;
    
    private int highestScore;
    private int playerNumber;

    public void DisplayScore()
    {
        gameManager.GetWinnerInfo(highestScore, playerNumber);
        // change the text of the canvas
    }

    public void CountdownTimer()
    {
        
    }
}
