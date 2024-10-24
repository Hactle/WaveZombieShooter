using UnityEngine;

namespace Factory
{
    internal interface IEnemy
    {
        void MoveTo(Vector3 position);
        void AttackPlayer();
    }
}