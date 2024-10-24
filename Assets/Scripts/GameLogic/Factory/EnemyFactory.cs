using HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    WeaknessEnemy
}

namespace Factory
{
    public class EnemyFactory : MonoBehaviour
    {
        public static EnemyFactory Instance;

        [SerializeField] private EnemyData[] _enemyDataArray;

        [SerializeField] private float _mapMinX = -50f;
        [SerializeField] private float _mapMaxX = 50f;
        [SerializeField] private float _mapMinZ = -50f;
        [SerializeField] private float _mapMaxZ = 50f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                Destroy(gameObject);
        }

        public GameObject CreateEnemy(EnemyType enemyType)
        {
            Vector3 spawnPosition = GetRandomPosition();

            EnemyData enemyData = GetEnemyDataByType(enemyType);

            GameObject enemy = Instantiate(enemyData.EnemyPrefab, spawnPosition, Quaternion.identity);
            enemy.GetComponent<Health>().SetHealth(enemyData.Healh);
            enemy.GetComponent<Zombie>().SetParameters(enemyData.Damage, enemyData.AttackColdown);

            return enemy;
        }

        private EnemyData GetEnemyDataByType(EnemyType type)
        {
            foreach (var data in _enemyDataArray)
            {
                if (data.enemyType == type)
                {
                    return data;
                }
            }

            return null;
        }

        private Vector3 GetRandomPosition()
        {
            int edge = Random.Range(0, 4);

            Vector3 spawnPosition = Vector3.zero;

            switch (edge)
            {
                case 0:
                    spawnPosition = new Vector3(Random.Range(_mapMinX, _mapMaxX), 0, _mapMaxZ);
                    break;
                case 1:
                    spawnPosition = new Vector3(Random.Range(_mapMinX, _mapMaxX), 0, _mapMinZ);
                    break;
                case 2:
                    spawnPosition = new Vector3(_mapMinX, 0, Random.Range(_mapMinZ, _mapMaxZ));
                    break;
                case 3:
                    spawnPosition = new Vector3(_mapMaxX, 0, Random.Range(_mapMinZ, _mapMaxZ));
                    break;
            }

            return spawnPosition;
        }
    }

}

