namespace Factory
{
    public class AttackState : IEnemyState
    {
        public void UpdateState(Zombie enemy)
        {
            if (!enemy.IsPlayerNearby())
            {
                enemy.TransitionToState(new ChaseState());
            }
            else
            {
                enemy.AttackPlayer();
            }
        }
    }
}