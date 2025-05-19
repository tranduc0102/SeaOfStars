using CoreActor;
using DG.Tweening;
using UnityEngine;

namespace Player.Bowman
{
    public class BowmanAttack : IAttackBehaviour
    {
        private Actor _owner;

        private Actor _target; 
        private GameObject _projectilePrefab; 
        private Transform _spawnPosition;
        private Vector3 _originPos;

        public BowmanAttack(Actor owner)
        {
            this._owner = owner;
        }
        public void SetupReference(GameObject projectilePrefab, Transform spawnPosition)
        {
            _projectilePrefab = projectilePrefab;
            _spawnPosition = spawnPosition;
        }
        private void SetActorTarget()
        {
            _target = ControllerGamePlay.Instance.TargetEnemy;
        }

        private void OnAttackAnimationEvent()
        {
            ObseverAnimationEvent.Instance.actionEnter -= OnAttackAnimationEvent;

            if (_projectilePrefab != null && _spawnPosition != null)
            {
                GameObject projectile =
                    Object.Instantiate(_projectilePrefab, _spawnPosition.position, Quaternion.identity);
                projectile.transform.LookAt(_target.transform.position);

                if (projectile.TryGetComponent(out Rigidbody rb))
                {
                    rb.AddForce(projectile.transform.forward * 500f);
                }
            }

            _target.StateActor.ChangeState(StateActorEnum.Hurt);
            _owner.StateActor.ChangeState(StateActorEnum.Idle);

            DOVirtual.DelayedCall(0.2f, () => ControllerGamePlay.Instance.NextTurn());
        }

        private void PlayAttackAnimation()
        {
            /*if (attacker is MainCharacter)
            {
                /*string trigger = _idAttack switch
                {
                    0 => "NormalAttack",
                    1 => "Skill1",
                    2 => "Skill2",
                    _ => "NormalAttack"
                };#1#*/
            _owner.Animator.SetTrigger("NormalAttack");
        }

        private void OnAttackAnimationExit()
        {
            if (_owner != null)
            {
                _owner.transform.DOJump(_originPos, 2f, 1, 0.5f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => { _target = null; });
            }
        }

        public void ExecuteAttack()
        {
            SetActorTarget();
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
                    ObseverAnimationEvent.Instance.actionExit += OnAttackAnimationExit;
                });
        }
    }
}