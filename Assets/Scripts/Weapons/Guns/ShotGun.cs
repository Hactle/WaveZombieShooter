using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class Shotgun : BasicPistol
    {
        [SerializeField] private int _pelletsPerShot = 5;
        [SerializeField] private float _spreadAngle = 30.0f;
        [SerializeField] private float _positionSpread = 0.2f;

        private Coroutine _reloadCoroutine;
        public override void Shoot()
        {
            if (_nextFireTime > Time.time)
                return;

            if (CurrentAmmo == 0 && !_isReloading)
            {
                Reload();
            }
            else if (_isReloading && CurrentAmmo != 0)
            {
                _isReloading = false;
                StopCoroutine(_reloadCoroutine);

                FireShotgunPellets();
                _nextFireTime = Time.time + _fireRate;
                CurrentAmmo--;
            }
            else if (CurrentAmmo > 0)
            {
                FireShotgunPellets();
                _nextFireTime = Time.time + _fireRate;
                CurrentAmmo--;
            }
        }

        public override void Reload()
        {
            if (CurrentAmmo == MaxAmmo)
                return;

            _reloadCoroutine = StartCoroutine(ReloadOne());
        }
        private IEnumerator ReloadOne()
        {
            _isReloading = true;

            while (CurrentAmmo < MaxAmmo)
            {
                yield return new WaitForSeconds(ReloadTime);

                if (CanShoot())
                {
                    Debug.Log("Reload interrupted for shooting.");
                    _isReloading = false;
                    yield break;
                }

                CurrentAmmo++;
                Debug.Log($"Reloaded one bullet. Ammo: {CurrentAmmo}/{MaxAmmo}");
            }

            Debug.Log("Shotgun fully reloaded.");
            _isReloading = false;
        }


        private void FireShotgunPellets()
        {
            for (int i = 0; i < _pelletsPerShot; i++)
            {
                Vector3 randomPositionOffset = new Vector3(
                    Random.Range(-_positionSpread, _positionSpread),
                    Random.Range(-_positionSpread, _positionSpread),
                    0
                );

                Vector3 spawnPosition = _firePoint.position + randomPositionOffset;

                float randomAngle = Random.Range(-_spreadAngle / 2, _spreadAngle / 2);
                Quaternion rotation = Quaternion.Euler(0, randomAngle, 0) * _firePoint.rotation;

                GameObject pellet = _bulletPool.GetBullet(spawnPosition, rotation);
                Bullet bulletComponent = pellet.GetComponent<Bullet>();

                bulletComponent?.Initialize(rotation * Vector3.forward, _bulletPool, _bulletDamage);
            }
        }
    }
}