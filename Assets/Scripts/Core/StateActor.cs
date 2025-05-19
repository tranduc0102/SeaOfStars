using UnityEngine;

namespace CoreActor
{
    public class StateActor
    {
        private IState currentState;
        private readonly IdleState _idleState;
        private readonly AttackState _attackState;
        private readonly HurtState _hurtState;
        private readonly DieState _dieState;
        private Actor _owner;

        public StateActor(Actor owner)
        {
            this._owner = owner;
            _idleState = new IdleState();
            _attackState = new AttackState(_owner);
            _hurtState = new HurtState(_owner);
            _dieState = new DieState();
            ChangeState(StateActorEnum.Idle);
        }

        public void ChangeState(StateActorEnum actorEnum, int idAttack = 0)
        {
            currentState?.Exit();
            currentState = CreateState(actorEnum, idAttack);

            currentState?.Enter();
            currentState?.Execute();
        }

        private IState CreateState(StateActorEnum actorEnum, int idAttack)
        {
            switch (actorEnum)
            {
                case StateActorEnum.Idle:
                    return _idleState;
                case StateActorEnum.Attack:
                    return _attackState;
                case StateActorEnum.Hurt:
                    return _hurtState;
                case StateActorEnum.Die:
                    return _dieState;
                default:
                    Debug.LogWarning($"Không có trạng thái phù hợp với enum {actorEnum}");
                    return null;
            }
        }
    }

    public enum StateActorEnum
    {
        Idle,
        Attack,
        Hurt,
        Die
    }
}