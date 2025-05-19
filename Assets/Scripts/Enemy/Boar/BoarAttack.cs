using CoreActor;
using DG.Tweening;
using UnityEngine;

namespace Enemy.Boar
{
    public class BoarAttack : IAttackBehaviour
    {
        private Actor _owner;
        private Actor _target;
        private Vector3 _originPos;
        public BoarAttack(Actor owner)
        {
            this._owner = owner;
            _target = ControllerGamePlay.Instance.CurrentCharacter;
        }

        public void ExecuteAttack()
        {
            if(_owner == null) return;
            _originPos = _owner.transform.position;
            Vector3 jumpTarget = new Vector3(_owner.transform.position.x, _owner.transform.position.y,
                _target.transform.position.z);

            _owner.transform
                .DOJump(jumpTarget, 2f, 1, 0.5f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    PlayAttackAnimation();
                    ObseverAnimationEvent.Instance.actionEnter += OnAttackAnimationEvent;
                    ObseverAnimationEvent.Instance.actionExit = null;
                    ObseverAnimationEvent.Instance.actionExit += OnAttackAnimationExit;
                });
        }
        private void PlayAttackAnimation()
        {
            _owner.Animator.SetTrigger("NormalAttack");
        }
        private void OnAttackAnimationExit()
        {
            if (_owner != null)
            {
                _owner.transform.DOJump(_originPos, 2f, 1, 0.5f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        DOVirtual.DelayedCall(0.5f, () =>
                        {
                            ControllerGamePlay.Instance.NextTurn();
                        });
                    });
            }
        }

        private void OnAttackAnimationEvent()
        {
            _owner.transform.DOMove(new Vector3(_target.transform.position.x + 0.8f, _target.transform.position.y, _target.transform.position.z), 0.25f).OnComplete(
                delegate
                {
                    _target.StateActor.ChangeState(StateActorEnum.Hurt);
                    _owner.StateActor.ChangeState(StateActorEnum.Idle);
                });
           
        }
    }
}