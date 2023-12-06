using System;
using Player;
using Spawners;
using UnityEngine;

namespace Inputs
{
    public class InputProvider : MonoBehaviour
    {
        private PlayerSpawner _playerSpawner;
        private Rotate _rotate;
        
        private readonly int _leftAxis = -2;
        private readonly int _rightAxis = 2;

        public void Init(PlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
            _playerSpawner.OnSpawnPlayer += SetPlayer;
        }

        private void OnDestroy()
        {
            if(_playerSpawner != null) _playerSpawner.OnSpawnPlayer -= SetPlayer;
        }

        private void SetPlayer(Rotate player) => _rotate = player;
        
        private void Update()
        {
            if (_rotate == null) return;
            Move();
        }

        private void Move()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _rotate.Rotation(_leftAxis);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _rotate.Rotation(_rightAxis);
            }
        }
    }
}