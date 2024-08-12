using UnityEngine;

namespace IdleClicker
{
    [CreateAssetMenu(menuName = "TrainingToolSO")]
    public class TrainingToolSO : ScriptableObject
    {
        // Variables

        [SerializeField] private float cost;
        [SerializeField] private float earningPerLift;

        // Properties

        public float Cost => cost;
        public float EarningPerLift => earningPerLift;

        // Methods
    }
}
