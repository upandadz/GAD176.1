using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent gameOver;
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

    private void StartGame()
    {
        // do count down 3 2 1 - coroutine
        // game starts - unfreeze players
    }

    public void GetWinnerInfo(int score, int playerNumber)
    {
        score = highestScore;
        playerNumber = playerWinnerNumber;
    }
    
    // keep track of time
    
    // round start
    
    // list of players to count how many spears stuck in them/how much damage they took, use for loop to count spears when timers out & see which is highest?
    public void CountScore()
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

    public void FreezePlayers()
    {
        foreach (Player player in players)
        {
            player.GetComponent<Movement>().frozen = true;
        }
    }
}
