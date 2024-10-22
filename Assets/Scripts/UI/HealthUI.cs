using UnityEngine;
using TMPro;
using HealthSystem;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Health _playerHealth;
        [SerializeField] private TMP_Text _healthText;

        private void Start()
        {
            _healthText.text = $"{_playerHealth.MaxHealth} / {_playerHealth.MaxHealth}";
        }

        private void OnEnable()
        {
            _playerHealth.OnHealthChanged += UpdateHealthUI;
        }

        private void OnDisable()
        {
            _playerHealth.OnHealthChanged -= UpdateHealthUI;
        }

        private void UpdateHealthUI(int changedHealth)
        {
            _healthText.text = $"{changedHealth} / {_playerHealth.MaxHealth}";
        }
    }

}
