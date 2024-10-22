using System;
using System.Collections.Generic;
using UnityEngine;
using HealthSystem;
using PlayerController;
using UnityEngine.InputSystem;

namespace EntityBehaviour
{
    public class EntityDieLogic : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;

        public static EntityDieLogic instance;

        private List<Health> ActiveEntity = new List<Health>();

        public event Action OnPlayerDie;

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
                    OnPlayerDie?.Invoke();
                    _playerMovement.enabled = false;
                    _playerInput.enabled = false;
                    Debug.Log("Player die");
                    break;
                case EntityType.Enemy:
                    Health entityHealth = entity.GetComponent<Health>();
                    if (entityHealth != null && ActiveEntity.Contains(entityHealth))
                    {
                        RemoveEntity(entityHealth);
                        Destroy(entity);
                    }
                    break;
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
