using DesignPattern;
using DG.Tweening;
using UnityEngine;

namespace CoreActor
{
    public class ControllerGamePlay : Singleton<ControllerGamePlay>
    {
        public Enemy TargetEnemy;
        public MainCharacter[] CurrentCharacter;
        private int indexPlayer = 0;
        public bool IsPlayerTurn { get; private set; } = true;

        private void Start()
        {
            TargetEnemy.InitData("Boar");

            foreach (var vCharacter in CurrentCharacter)
            {
                vCharacter.InitData(vCharacter.GetType().Name);
            }

            ShowUIPlayer(indexPlayer);
        }

        private void Update()
        {
            if (!IsPlayerTurn) return;

            if (Input.GetKeyDown(KeyCode.A))
            {
                ChangePlayer(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ChangePlayer(1);
            }
        }

        public void NextTurn()
        {
            HideUIPlayer(indexPlayer);
            CurrentCharacter[indexPlayer].IsTurn = true;
            for (int i = 0; i < CurrentCharacter.Length; i++)
            {
                if (!CurrentCharacter[i].IsTurn)
                {
                    indexPlayer = i;
                    ShowUIPlayer(indexPlayer);
                    return;
                }
            }
            IsPlayerTurn = !IsPlayerTurn;

            if (IsPlayerTurn)
            {
                for (int i = 0; i < CurrentCharacter.Length; i++)
                {
                    if (CurrentCharacter[i].IsTurn)
                    {
                        CurrentCharacter[i].IsTurn = false;
                    }
                }
                Debug.LogError("1");
                indexPlayer = 0;
                ShowUIPlayer(indexPlayer);
            }
            else
            {
                HideUIPlayer(indexPlayer);
                TargetEnemy.StateActor.ChangeState(StateActorEnum.Attack);
            }
        }
        private void ChangePlayer(int dir)
        {
            int originalIndex = indexPlayer;
            int newIndex = indexPlayer;

            for (int i = 0; i < CurrentCharacter.Length; i++)
            {
                newIndex = (newIndex + dir + CurrentCharacter.Length) % CurrentCharacter.Length;
                if (!CurrentCharacter[newIndex].IsTurn)
                {
                    break;
                }
            }

            if (newIndex == originalIndex || CurrentCharacter[newIndex].IsTurn)
            {
                return;
            }

            HideUIPlayer(indexPlayer);
            indexPlayer = newIndex;
            ShowUIPlayer(indexPlayer);
        }


        private void ShowUIPlayer(int index)
        {
            var ui = CurrentCharacter[index].UIActionCharacter;
            ui.gameObject.SetActive(true);
            ui.DOFade(1f, 0.3f);
        }

        private void HideUIPlayer(int index)
        {
            var ui = CurrentCharacter[index].UIActionCharacter;
            ui.DOFade(0f, 0.3f).OnComplete(() => ui.gameObject.SetActive(false));
        }
    }
}
