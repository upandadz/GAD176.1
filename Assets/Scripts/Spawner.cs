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
        Instantiate(prefabsList.throwables[Random.Range(0, prefabsList.throwables.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
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
