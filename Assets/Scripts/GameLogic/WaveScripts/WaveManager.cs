using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory;
using HealthSystem;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveData[] _waves;
    [SerializeField] private float _timeBetweenWaves = 30f;

    private int _currentWaveIndex = 0;
    private List<GameObject> _activeEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        if (_currentWaveIndex >= _waves.Length)
        {
            Debug.Log("All waves completed!");
            yield break;
        }

        WaveData currentWave = _waves[_currentWaveIndex];
        _currentWaveIndex++;

        Debug.Log($"Starting Wave {_currentWaveIndex}");

        for (int i = 0; i < currentWave.EnemyCount; i++)
        {
            EnemyType enemyType = currentWave.EnemyTypes[Random.Range(0, currentWave.EnemyTypes.Length)];
            GameObject enemy = EnemyFactory.Instance.CreateEnemy(enemyType);
            _activeEnemies.Add(enemy);

            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.OnDeath += OnEnemyDeath;
            }

            yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);
        }

        yield return null;
    }

    private void OnEnemyDeath(EntityType type, GameObject enemy)
    {
        if (type == EntityType.Enemy)
        {
            _activeEnemies.Remove(enemy);

            if (_activeEnemies.Count == 0)
            {
                Debug.Log("All enemies defeated. Next wave incoming!");
                StartCoroutine(StartNextWaveWithDelay());
            }
        }
    }

    private IEnumerator StartNextWaveWithDelay()
    {
        yield return new WaitForSeconds(_timeBetweenWaves);
        StartCoroutine(StartNextWave());
    }
}

