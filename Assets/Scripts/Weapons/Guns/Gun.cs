using System;
using System.Collections;
using UnityEngine;

namespace Weapons
{
    public abstract class Gun : MonoBehaviour, IWeapon
    {
        [SerializeField] private int _maxAmmo = 30;
        [SerializeField] private float _reloadTime = 3f;
        [SerializeField, Range(0.01f, 3f)] protected float _fireRate = 0.5f;
        [SerializeField, Range(1, 1000)] protected int _bulletDamage = 10;
        [SerializeField] protected BulletPool _bulletPool;

        protected Transform _firePoint;
        protected float _nextFireTime;
        private int _currentAmmo;

        public float ReloadTime => _reloadTime;

        public int CurrentAmmo
        {
            get { return _currentAmmo; }
            set
            {
                _currentAmmo = value;
                OnAmmoChanged?.Invoke(value);
            }
        }

        protected bool _isReloading = false;

        public int MaxAmmo => _maxAmmo;

        public event Action<int> OnAmmoChanged;

        private void Awake()
        {
            _firePoint = GetComponentInChildren<Transform>();
        }

        private void Start()
        {
            CurrentAmmo = _maxAmmo;
            _nextFireTime = 0f;
        }

        public virtual void Shoot()
        {
            if (CanShoot() && Time.time >= _nextFireTime)
            {
                FireBullet();
                _nextFireTime = Time.time + _fireRate;
                CurrentAmmo--;
            }
            else if (CurrentAmmo == 0)
            {
                Reload();
            }
        }

        public virtual void Reload()
        {
            if (!_isReloading && _currentAmmo < _maxAmmo)
            {
                StartCoroutine(ReloadCoroutine());
            }
        }

        protected IEnumerator ReloadCoroutine()
        {
            _isReloading = true;
            Debug.Log($"Reloading... ({_reloadTime} seconds)");
            yield return new WaitForSeconds(_reloadTime);

            CurrentAmmo = _maxAmmo;
            Debug.Log("Reload complete.");
            _isReloading = false;
        }

        protected void FireBullet()
        {
            GameObject bullet = _bulletPool.GetBullet(_firePoint.position, _firePoint.rotation);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();

            bulletComponent?.Initialize(_firePoint.forward, _bulletPool, _bulletDamage);
        }

        public bool CanShoot()
        {
            return CurrentAmmo > 0 && !_isReloading;
        }
    }
}
