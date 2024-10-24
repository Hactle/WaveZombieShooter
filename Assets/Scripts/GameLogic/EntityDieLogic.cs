using System;
using System.Collections.Generic;
using UnityEngine;
using HealthSystem;
using PlayerController;
using UnityEngine.InputSystem;
using Factory;

namespace EntityBehaviour
{
    public class EntityDieLogic : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;

        public static EntityDieLogic instance;

        private List<Health> ActiveEntity = new List<Health>();

        public event Action OnPlayerDie;

        [SerializeField, Range(0f, 1f)] private float _silverCoinDropChance = 0.6f;
        [SerializeField, Range(0f, 1f)] private float _goldenCoinDropChance = 0.2f;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
                Destroy(gameObject);

            _playerInput = FindFirstObjectByType<PlayerInput>();
            _playerMovement = FindFirstObjectByType<PlayerMovement>();
        }

        public void AddEntity(Health entity)
        {
            if (!ActiveEntity.Contains(entity))
            {
                ActiveEntity.Add(entity);
                entity.OnDeath += EntityDie;
            }
        }

        public void RemoveEntity(Health entity)
        {
            entity.OnDeath -= EntityDie;
            ActiveEntity.Remove(entity);
        }

        private void EntityDie(EntityType type, GameObject entity)
        {
            switch (type)
            {
                case EntityType.Player:
                    PlayerDeath();
                    break;
                case EntityType.Enemy:
                    EnemyDeath(entity);
                    break;
            }
        }

        private void EnemyDeath(GameObject entity)
        {
            Health entityHealth = entity.GetComponent<Health>();
            if (entityHealth != null && ActiveEntity.Contains(entityHealth))
            {
                DropLoot(entity.transform.position);
                RemoveEntity(entityHealth);
                Destroy(entity);
            }
        }

        private void PlayerDeath()
        {
            OnPlayerDie?.Invoke();
            _playerMovement.enabled = false;
            _playerInput.enabled = false;
            Debug.Log("Player die");
        }

        private void DropLoot(Vector3 position)
        {
            float randomValue = UnityEngine.Random.Range(0f, 1f);

            if (randomValue <= _goldenCoinDropChance)
            {
                GoldenCoinFactory.Instance.CreateGoldenCoin(position);
                Debug.Log("Gold coin dropped!");
            }
            else if (randomValue <= _silverCoinDropChance)
            {
                SilverCoinFactory.Instance.CreateSilverCoin(position);
                Debug.Log("Silver coin dropped!");
            }
            else
            {
                Debug.Log("No coin dropped.");
            }
        }

        private void OnDisable()
        {
            foreach (var entityHealth in ActiveEntity)
            {
                if (entityHealth != null)
                {
                    entityHealth.OnDeath -= EntityDie;
                }
            }
        }
    }
}
