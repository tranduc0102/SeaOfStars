using CoreActor;

namespace Enemy.Boar
{
    public class Boar : CoreActor.Enemy
    {
        public override void InitData(string dataName)
        {
            base.InitData(dataName);
            AttackBehaviour = EnemyAttackFactory.Create(this);
        }
    }
}