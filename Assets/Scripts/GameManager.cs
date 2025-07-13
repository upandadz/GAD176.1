using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent gameOver;
    public UnityEvent gameStart;
    public static GameManager gameManager;
    
    [SerializeField] private List<Player> players;
    [SerializeField] private float roundTime;
    
    private int scoreLead;
    private int playerWinnerNumber;
    private float countdowntime = 3f;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    }

    void Start()
    {
        StartCoroutine(CountDown(countdowntime));
        StartCoroutine(RoundTimer(roundTime));
    }

    public void GetWinnerInfo(out int score, out int playerNumber)
    {
        score = scoreLead;
        playerNumber = playerWinnerNumber;
    }
    
    // keep track of time
    
    // round start
    
    // list of players to count how many spears stuck in them/how much damage they took, use foreach loop to count spears when timers out & see which is highest?
    public void CountScore()
    {
        int highestScore = 0;
        int lowestScore = int.MaxValue;
        
        foreach (Player player in players)
        {
            int score = player.CalculateFinalScore();
            
            if (score > highestScore)
            {
                highestScore = score;
            }
            
            if (score < lowestScore)
            {
                lowestScore = score;
                playerWinnerNumber = player.GetPlayerNumber();
            }
        }
        
        scoreLead = highestScore - lowestScore;
    }

    public float GetRoundTime()
    {
        return roundTime;
    }
    public void FreezePlayers()
    {
        foreach (Player player in players)
        {
            player.GetComponent<Movement>().frozen = true;
        }
    }

    public void UnfreezePlayers()
    {
        foreach (Player player in players)
        {
            player.GetComponent<Movement>().frozen = false;
        }
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    private IEnumerator CountDown(float seconds)
    {
        FreezePlayers();
        yield return new WaitForSeconds(seconds);
        UnfreezePlayers();
        gameStart.Invoke();
    }

    private IEnumerator RoundTimer(float roundTime)
    {
        float timeRemaining = roundTime += countdowntime; // was having an issue where it would start counting down before the round start countdown was finished, this is just to offset that
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null; // waits until the next frame to run again, saves me doing this in update
        }
        FreezePlayers();
        gameOver.Invoke();
    }
}
