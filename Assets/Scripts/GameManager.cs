using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    }
    // keep track of time
    // round start
    // list of players to count how many spears stuck in them/how much damage they took
}
