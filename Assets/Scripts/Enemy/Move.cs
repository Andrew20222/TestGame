using Player;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Move : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        private Rotate _player;

        private readonly int _leftAxis = -1;
        private readonly int _rightAxis = 1;

        public void Init(Rotate playerRotate)
        {
            _player = playerRotate;   
            SetSpeed();
            SetRotation();
        }
        
        private void SetSpeed() => speed *= transform.position.x > 0 ? _leftAxis : _rightAxis;

        private void SetRotation()
        {
            var enemyTransform = transform;
            var scale = enemyTransform.localScale;
            scale.x *= enemyTransform.position.x > 0 ? _leftAxis : _rightAxis;;
            transform.localScale = scale;
        }

        private void Update()
        {
            if(_player == null) return;
            Movement();
        } 
        
        private void Movement() => rb.velocity = 
            new Vector2(_player.transform.position.x, _player.transform.position.y) * speed;
    }
}
