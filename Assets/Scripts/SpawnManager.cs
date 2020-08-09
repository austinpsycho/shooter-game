using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject powerUpContainer;
    private Player player;
    private void Start()
    {
        player = GameObject.Find("Player")?.GetComponent<Player>();
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (player != null)
        {
            Instantiate(enemy).transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        while (player != null)
        {
            if (powerUps.Any())
                Instantiate(powerUps[Random.Range(0, powerUps.Length)]).transform.parent = powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f,7f));
        }
    }
}
