using UnityEngine;

namespace DamageField
{
    public class DamageField : MonoBehaviour
    {
        [SerializeField] private float damage;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent(out Health.Health enemy))
            {
                enemy.Subtract(damage);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent(out Health.Health enemy))
            {
                enemy.Subtract(damage);
            }
        }
    }
}
