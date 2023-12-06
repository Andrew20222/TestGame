using System;
using Player;
using UnityEngine;

namespace Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        public event Action<Rotate> OnSpawnPlayer;
        
        [SerializeField] private Rotate playerPrefab;
        [SerializeField] private Transform spawnPoint;

        public void Spawn()
        {
            var player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            OnSpawnPlayer?.Invoke(player);
        }
    }
}