using System;
using System.Collections.Generic;
using Enemy;
using Spawners;
using UnityEngine;

namespace Stats
{
    public class Score : MonoBehaviour
    {
        public event Action<float> OnChangeCountDieEnemy;

        [SerializeField] private List<Health.Health> _healths;
        private EnemySpawner _enemySpawner;
        private int _countDieEnemy;

        private readonly string _keySave = "Score";

        public void Init(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
            _enemySpawner.OnSpawnEnemy += SetEnemy;
        }

        private void SetEnemy(Move enemy)
        {
            if(enemy == null) return;
            if (enemy.TryGetComponent(out Health.Health health))
            {
                _healths.Add(health);
                _countDieEnemy = PlayerPrefs.GetInt(_keySave);
                OnChangeCountDieEnemy?.Invoke(_countDieEnemy);
                var index = _healths.IndexOf(enemy.GetComponent<Health.Health>());
                _healths[index].OnDie += CheckScore;
            }
        }

        private void OnDestroy()
        {
            if (_healths == null && _healths.Count == 0)
            {
                foreach (var health in _healths)
                {
                    health.OnDie -= CheckScore;
                }    
            }
            _enemySpawner.OnSpawnEnemy -= SetEnemy;
        }

        private void CheckScore(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out Move enemy))
            {
                _countDieEnemy++;
                OnChangeCountDieEnemy?.Invoke(_countDieEnemy);
                PlayerPrefs.SetInt(_keySave, _countDieEnemy);
            }
        }
    }
}