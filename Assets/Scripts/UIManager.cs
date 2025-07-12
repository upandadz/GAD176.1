using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private int highestScore;
    private int playerNumber;

    public void DisplayScore()
    {
        gameManager.GetWinnerInfo(highestScore, playerNumber);
    }
}
