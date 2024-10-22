using UnityEngine;
using HealthSystem;

namespace Weapons
{
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rb;
        private BulletPool _bulletPool;

        private int _damage;
        private float _speed = 30f;
        private float _lifeTime = 2f; 

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Initialize(Vector3 direction, BulletPool pool, int damage)
        {
            _damage = damage;
            _bulletPool = pool;
     
            _rb.velocity = direction.normalized * _speed;
            RotateBullet(direction);

            Invoke(nameof(ReturnToPool), _lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Health>().TakeDamage(_damage);
                ReturnToPool();  

            }                                      
            if (other.CompareTag("Wall"))
                ReturnToPool();
        }

        private void RotateBullet(Vector3 direction)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);

            rotation *= Quaternion.Euler(90, 0, 0);

            transform.rotation = rotation;
        }

        private void ReturnToPool()
        {
            _rb.velocity = Vector3.zero;

            CancelInvoke(nameof(ReturnToPool));

            _bulletPool.ReturnBullet(gameObject);
        }
    }
}
