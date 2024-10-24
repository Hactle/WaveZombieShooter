using UnityEngine;
using Player;

public class Coin : MonoBehaviour
{
    [SerializeField, Range(0, 999)] private int _coinValue = 25;

    private PlayerWallet _wallet;

    private void Awake()
    {
        _wallet = FindAnyObjectByType<PlayerWallet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickupCoin();
        }
    }

    public void PickupCoin()
    {
        _wallet.AddMoney(_coinValue);
        Destroy(gameObject);
    }
}
