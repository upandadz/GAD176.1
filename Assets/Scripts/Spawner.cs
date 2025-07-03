using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private PrefabsList prefabsList;

    public void Spawn()
    {
        // Instantiate prefab list item at random range spawn point position
    }
   
}
