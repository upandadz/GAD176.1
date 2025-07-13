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
    
    private int highestScore;
    private int playerWinnerNumber;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    }

    void Start()
    {
        StartCoroutine(CountDown(3f));
        StartCoroutine(RoundTimer(roundTime));
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
    }

    private IEnumerator RoundTimer(float roundTime)
    {
        float timeRemaining = roundTime;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        FreezePlayers();
        gameOver.Invoke();
    }
}
