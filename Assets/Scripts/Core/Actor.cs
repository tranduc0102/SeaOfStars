using UnityEngine;

namespace CoreActor
{
    [RequireComponent(typeof(Animator))]
    public abstract class Actor : MonoBehaviour
    {
        public float HP { get; set; }
        public float Damage { get; set; }
        public float Armor { get; set; }
        public float CritChance { get; set; }

        public Animator Animator { get; private set; }
        public StateActor StateActor { get; private set; }
        public IAttackBehaviour AttackBehaviour { get; protected set; }

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            StateActor = new StateActor(this);
        }

        public virtual void InitData(string dataName)
        {
            DataCharacter data = Resources.Load<DataCharacter>(dataName);
            if (data == null)
            {
                Debug.LogError($"Không tìm thấy DataCharacter với tên '{dataName}' trong Resources.");
                return;
            }

            HP = data.hp;
            Armor = data.armor;
            Damage = data.damage;
            CritChance = data.critChance;

            if (Animator != null && data.animatorController != null)
                Animator.runtimeAnimatorController = data.animatorController;
        }
    }
}