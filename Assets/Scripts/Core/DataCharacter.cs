using UnityEngine;

namespace CoreActor
{
    [CreateAssetMenu(menuName = "DataSO/DataCharacter", fileName = "DataCharacter")]
    public class DataCharacter : ScriptableObject
    {
        public RuntimeAnimatorController animatorController;
        public float hp;
        public float damage;
        public float armor;
        public float critChance;
    }
}