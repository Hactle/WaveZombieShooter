using UnityEngine;

namespace Factory
{
    public class GoldenCoinFactory : MonoBehaviour
    {
        public static GoldenCoinFactory Instance;

        [SerializeField] private GameObject _goldenCoinPrefab;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                Destroy(gameObject);
        }

        public GameObject CreateGoldenCoin(Vector3 spawnPosition)
        {
            return Instantiate(_goldenCoinPrefab, spawnPosition, Quaternion.Euler(150, 0, 180));
        }
    }
}

