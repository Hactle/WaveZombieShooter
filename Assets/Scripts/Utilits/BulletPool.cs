using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private int _initialPoolSize = 30;

        private Queue<GameObject> _pool = new Queue<GameObject>();

        private void Awake()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                GameObject bullet = Instantiate(_bulletPrefab);
                bullet.SetActive(false);
                _pool.Enqueue(bullet);
            }
        }

        public GameObject GetBullet(Vector3 position, Quaternion rotation)
        {
            if (_pool.Count > 0)
            {
                GameObject bullet = _pool.Dequeue();
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
                bullet.SetActive(true);
                return bullet;
            }
            else
            {
                GameObject bullet = Instantiate(_bulletPrefab, position, rotation);
                return bullet;
            }
        }

        public void ReturnBullet(GameObject bullet)
        {
            bullet.SetActive(false);
            _pool.Enqueue(bullet);
        }
    }
}
