using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text playerXWonText;
    [SerializeField] private TMP_Text xPointLead;
    
    private int scoreLead;
    private int playerNumber;
    
    public void DisplayScore()
    {
        gameManager.GetWinnerInfo(out scoreLead, out playerNumber);
        // change the text of the canvas
        playerXWonText.text = "Player " + playerNumber + " won";
        xPointLead.text = scoreLead + " point lead";
    }

    public void StartCountDownTimer()
    {
        StartCoroutine(CountdownTimer());
    }
    private IEnumerator CountdownTimer()
    {
        float timeRemaining = gameManager.GetRoundTime();
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = timeRemaining.ToString("00:00");
            yield return null;
        }
    }
}
