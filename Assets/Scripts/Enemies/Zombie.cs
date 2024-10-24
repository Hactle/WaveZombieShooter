using UnityEngine;
using UnityEngine.AI;
using HealthSystem;

namespace Factory
{
    public abstract class Zombie : MonoBehaviour, IEnemy
    {
        [SerializeField] private float _attackRange = 1f;

        private int _damage;
        private float _attackCooldown = 2f;

        protected NavMeshAgent _agent;
        protected IEnemyState _currentState;
        protected float _lastAttackTime;
        
        public Transform _player;
      
        private void Awake()
        {
            GameObject playerObject = GameObject.FindWithTag("Player");         
            _player = playerObject.transform;     
            
            _agent = GetComponent<NavMeshAgent>();
            _currentState = new ChaseState();
        }

        public void SetParameters(int damage, float attackColdown)
        {
            _damage = damage;
            _attackCooldown = attackColdown;
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        public void MoveTo(Vector3 positon)
        {
            _agent.SetDestination(positon);
        }

        internal void Die()
        {
            Destroy(gameObject);
        }

        public void AttackPlayer()
        {
            if(Time.time >= _lastAttackTime + _attackCooldown)
            {
                _player.gameObject.GetComponent<Health>().TakeDamage(_damage);
                Debug.Log("Зондбе ударил пиздец щас откинусь");
                _lastAttackTime = Time.time;
            }
        }

        public void TransitionToState(IEnemyState newState)
        {
            _currentState = newState;
        }

        public bool IsPlayerNearby()
        {
            return Vector3.Distance(_player.position, transform.position) <= _attackRange;
        }

    }











}