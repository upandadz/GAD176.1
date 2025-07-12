using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private int highestScore;
    private int playerWinnerNumber;
    
    [SerializeField] private List<Player> players;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    }
    
    // keep track of time
    
    // round start
    
    // list of players to count how many spears stuck in them/how much damage they took, use for loop to count spears when timers out & see which is highest?
    private void CountScore()
    {
        foreach (Player player in players)
        {
            int score = player.CalculateFinalScore();
            if (score > highestScore)
            {
                highestScore = score;
                playerWinnerNumber = player.GetPlayerNumber();
            }
        }
    }
}
