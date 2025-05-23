using CoreActor;
using DG.Tweening;
using UnityEngine;

namespace CoreActor
{
    public class MainCharacter : Actor
    {
        public bool IsTurn { get; set; }
        public CanvasGroup UIActionCharacter;

        public void ActionAttack(int idAttack)
        {
            IsTurn = false;
            StateActor.ChangeState(StateActorEnum.Attack, idAttack);
            UIActionCharacter.DOFade(0, 0.2f).OnComplete(() => UIActionCharacter.gameObject.SetActive(false));
        }
    }
}