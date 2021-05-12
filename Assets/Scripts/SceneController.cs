using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;
    private GameObject _enemy;

    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void SpawnNewNPC()
    {
        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        _enemy = Instantiate(enemyPrefab) as GameObject;
        _enemy.transform.position = spawnPoint;
        float angle = Random.Range(0, 360);
        _enemy.transform.Rotate(0, angle, 0);
    }
}