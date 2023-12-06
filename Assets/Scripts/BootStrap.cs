using System;
using Enemy;
using Inputs;
using Player;
using Spawners;
using Stats;
using UnityEngine;
public class BootStrap : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Score score;
    [SerializeField] private InputProvider inputProvider;
    [SerializeField] private UI.UI ui;

    private Move _enemy;
    private Rotate _player;
    private void Start()
    {
        enemySpawner.Init();
        enemySpawner.OnSpawnEnemy += enemy => enemy.Init(_player);
        score.Init(enemySpawner);
        inputProvider.Init(playerSpawner);
        ui.Init(playerSpawner);
        playerSpawner.OnSpawnPlayer += player => _player = player;
        playerSpawner.Spawn();
    }
}