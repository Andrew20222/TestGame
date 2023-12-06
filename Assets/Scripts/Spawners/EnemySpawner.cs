using System;
using System.Collections;
using Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        public event Action<Move> OnSpawnEnemy; 
        public event Action<Health.Health> OnHealthEnemy; 
        
        [SerializeField] private Move enemyPrefab;
        [SerializeField] private Transform[] spawnPoint;
        [SerializeField] private float coolDown = 5f;
        private Coroutine _spawnEnemy;

        public void Init()
        {
            _spawnEnemy = StartCoroutine(SpawnEnemy());
        }

        private void OnDestroy()
        {
            if(_spawnEnemy != null) StopCoroutine(_spawnEnemy);
        }

        private void Spawn()
        {
            var enemy = Instantiate(enemyPrefab, 
                spawnPoint[Random.Range(0,spawnPoint.Length)].position, 
                Quaternion.identity);
            OnSpawnEnemy?.Invoke(enemy);
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(coolDown);
                Spawn();
            }
        }
    }
}