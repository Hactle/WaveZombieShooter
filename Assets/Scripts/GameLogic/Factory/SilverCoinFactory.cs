using UnityEngine;

namespace Factory
{
    public class SilverCoinFactory : MonoBehaviour
    {
        public static SilverCoinFactory Instance;
        
        [SerializeField] private GameObject _silverCoinPrefab;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                Destroy(gameObject);
        }

        public GameObject CreateSilverCoin(Vector3 spawnPosition)
        {
            return Instantiate(_silverCoinPrefab, spawnPosition, Quaternion.Euler(150, 0, 180));
        }
    }
}
