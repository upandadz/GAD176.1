using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    public static Action<Player> OnStoodOnNailsEvent;
    
    public static Action<Player> OnHitByProjectileEvent;
    // event spear lands in player
    // player spear count ++
    // spear spawns in
}
