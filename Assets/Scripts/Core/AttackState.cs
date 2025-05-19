using DesignPattern;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CoreActor
{
    public class AttackState : IState
    {
        private readonly Actor _attacker;

        public AttackState(Actor attacker)
        {
            this._attacker = attacker;
        }
        public void Enter()
        {
            _attacker.AttackBehaviour?.ExecuteAttack();
        }

        public void Execute() { }

        public void Exit()
        {
        }
    }
}
