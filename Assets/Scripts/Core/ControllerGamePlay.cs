using System;
using DesignPattern;
using UnityEngine;

namespace CoreActor
{
    public class ControllerGamePlay : Singleton<ControllerGamePlay>
    {
        public Enemy TargetEnemy;
        public MainCharacter CurrentCharacter;
        public GameObject prefab;
        public Transform pos;

        public bool IsPlayerTurn { get; private set; } = true;

        private void Start()
        {
            CurrentCharacter.InitData("BowMan");
            TargetEnemy.InitData("Boar");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CurrentCharacter.StateActor.ChangeState(StateActorEnum.Attack, 0);
            }
        }

        public void NextTurn()
        {
            IsPlayerTurn = !IsPlayerTurn;

            if (IsPlayerTurn)
            {
                Debug.Log("Player's Turn");
                CurrentCharacter.StateActor.ChangeState(StateActorEnum.Idle);
            }
            else
            {
                Debug.Log("Enemy's Turn");
                TargetEnemy.StateActor.ChangeState(StateActorEnum.Attack);
            }
        }
    }
}