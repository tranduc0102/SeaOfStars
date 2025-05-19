using CoreActor;

namespace Enemy
{
    public static class EnemyAttackFactory
    {
        public static IAttackBehaviour Create(Actor owner)
        {
            switch (owner)
            {
                case Boar.Boar:
                    return new Boar.BoarAttack(owner);
                default:
                    UnityEngine.Debug.LogWarning($"Chưa định nghĩa hành vi tấn công cho: {owner.GetType().Name}");
                    return null;
            }
        }
    }
}