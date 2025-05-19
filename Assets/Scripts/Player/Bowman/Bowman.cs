using CoreActor;
using UnityEngine;

namespace Player.Bowman
{
    public class Bowman : MainCharacter
    {
        [SerializeField] private GameObject _projectilePrefab; 
        [SerializeField] private Transform _spawnPosition;
        private void Start()
        {
            InitData("Bowman");
        }

        public override void InitData(string dataName)
        {
            base.InitData(dataName);
            AttackBehaviour = PlayerAttackFactory.Create(this);
            if (AttackBehaviour is BowmanAttack setup)
            {
                setup.SetupReference(_projectilePrefab, _spawnPosition);
            }
        }
    }
}
