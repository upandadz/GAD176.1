using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns in items
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private PrefabsList prefabsList;

    public void Spawn()
    {
        // Instantiate prefab list item at random range spawn point position
        Transform pointToSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Instantiate(prefabsList.throwables[Random.Range(0, prefabsList.throwables.Count)], pointToSpawn.position, Quaternion.identity, pointToSpawn);
    }

    private void OnEnable()
    {
        GameEvents.OnNailJarSmashed += Spawn;
        GameEvents.OnHitByProjectileEvent += Spawn;
    }

    private void OnDisable()
    {
        GameEvents.OnNailJarSmashed -= Spawn;
        GameEvents.OnHitByProjectileEvent -= Spawn;
    }
}
