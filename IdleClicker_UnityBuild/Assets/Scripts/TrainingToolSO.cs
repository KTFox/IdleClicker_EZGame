using UnityEngine;

namespace IdleClicker
{
    [CreateAssetMenu(menuName = "TrainingToolSO")]
    public class TrainingToolSO : ScriptableObject
    {
        // Variables

        [SerializeField] private string toolName;
        [SerializeField] private Sprite icon;
        [SerializeField] private float cost;
        [SerializeField] private float earningPerLift;

        // Properties

        public string ToolName => toolName;
        public Sprite Icon => icon;
        public float Cost => cost;
        public float EarningPerLift => earningPerLift;

        // Methods
    }
}
