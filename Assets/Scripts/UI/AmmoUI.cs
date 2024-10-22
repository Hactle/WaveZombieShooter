using UnityEngine;
using TMPro;
using Weapons;

namespace UI
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _ammoText;
        [SerializeField] private Gun _currentWeapon;

        private void Start()
        {
            _ammoText.text = $"{_currentWeapon.MaxAmmo} / {_currentWeapon.MaxAmmo}";
        }

        void UpdateCurrentAmmo(int changedAmmo)
        {
            _ammoText.text = $"{changedAmmo} / {_currentWeapon.MaxAmmo}";
        }

        private void OnEnable()
        {
            _currentWeapon.OnAmmoChanged += UpdateCurrentAmmo;
        }

        private void OnDisable()
        {
            _currentWeapon.OnAmmoChanged -= UpdateCurrentAmmo;
        }

    }
}
