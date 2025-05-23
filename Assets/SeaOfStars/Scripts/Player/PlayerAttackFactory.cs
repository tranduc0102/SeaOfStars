using CoreActor;

namespace Player
{
    public static class PlayerAttackFactory
    {
        public static IAttackBehaviour Create(Actor owner)
        {
            switch (owner)
            {
                case Bowman.Bowman:
                    return new Bowman.BowmanAttack(owner);
                default:
                    UnityEngine.Debug.LogWarning($"Chưa định nghĩa hành vi tấn công cho: {owner.GetType().Name}");
                    return null;
            }
        }
    }
}