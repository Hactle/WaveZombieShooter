using UnityEngine;

namespace Enemy
{
    internal interface IEnemy
    {
        void MoveTo(Vector3 position);
        void AttackPlayer();
    }
}