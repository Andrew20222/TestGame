using UnityEngine;

namespace Player
{
    public class Rotate : MonoBehaviour
    {
        public void Rotation(int axis)
        {
            var playerTransform = transform;
            var scale = playerTransform.localScale;
            scale.x = axis;
            playerTransform.localScale = scale;
        }
    }
}
