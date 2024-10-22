using System;
using UnityEngine;
using EntityBehaviour;

namespace HealthSystem
{
    public enum EntityType
    {
        Player,
        Enemy
    }

    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private EntityType _entityType;

        private int _maxHealth = 100;

        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        private int _currentHealth;

        public int CurrentHealth
        {
            get { return _currentHealth; }
            private set
            {
                _currentHealth = value;
                OnHealthChanged?.Invoke(value);
                if (value == 0 && !_isDead)
                {
                    _isDead = true;
                    OnDeath?.Invoke(_entityType, gameObject);
                }
            }
        }

        private bool _isDead = false;

        public event Action<int> OnHealthChanged;
        public event Action<EntityType, GameObject> OnDeath;

        private void Start()
        {    
            EntityDieLogic.instance.AddEntity(this);
            CurrentHealth = _maxHealth;
        }

        public void SetHealth(int health)
        {
            _maxHealth = health;
        }

        public void TakeDamage(int damage)
        {
            if (!_isDead)
            {
                CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            }

        }

        public void Heal(int amount)
        {
            if (!_isDead)
            {
                CurrentHealth = Mathf.Min(CurrentHealth + amount, _maxHealth);
            }
        }
    }
}