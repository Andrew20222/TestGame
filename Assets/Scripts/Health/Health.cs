using System;
using UnityEngine;

namespace Health
{
    public class Health : MonoBehaviour
    {
        public event Action<GameObject> OnDie;
        public event Action<float> OnChangeHealth;
        
        [SerializeField] private float maxHealth;
        private float _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
            OnChangeHealth?.Invoke(_currentHealth);
        }

        public void Subtract(float damage)
        {
            _currentHealth -= damage;
            OnChangeHealth?.Invoke(_currentHealth);
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            OnDie?.Invoke(gameObject);
            Destroy(gameObject);
        }
   
    }
}
