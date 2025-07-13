using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
/// <summary>
/// lists the controls for the player so it can be modified
/// </summary>
public class Controls : MonoBehaviour
{
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode jumpKey;
    public KeyCode throwKey;
    public KeyCode throwStraightKey;
    public KeyCode DashKey;
    
    // could be done better with unity's new input system, didn't take the time to learn it properly though
}
