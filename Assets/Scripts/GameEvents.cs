using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    public static event Action<Player> OnStoodOnNailsEvent;
    // score goes up
    public static event Action OnNailJarSmashed;
    // spawn in another item
    public static event Action<Player> OnHitByProjectileEvent;
    // event spear lands in player
    // player spear count ++
    // spear spawns in
}