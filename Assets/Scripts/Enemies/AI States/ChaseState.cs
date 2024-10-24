using System.Diagnostics;

namespace Factory
{
    public class ChaseState : IEnemyState
    {
        public void UpdateState(Zombie enemy)
        {
            if (enemy.IsPlayerNearby())
            {
                enemy.TransitionToState(new AttackState());
            }
            else
            {
                enemy.MoveTo(enemy._player.position);
            }
        }
    }
}