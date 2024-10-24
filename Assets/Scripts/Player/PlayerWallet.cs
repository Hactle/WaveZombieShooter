using UnityEngine;

namespace Player
{
    public class PlayerWallet: MonoBehaviour
    {
        [SerializeField] private int _currentMoney;

        public void AddMoney(int moneyToAdd)
        {
            _currentMoney += moneyToAdd;

            Debug.Log($"Current money: {_currentMoney}");
        }
    }
}
