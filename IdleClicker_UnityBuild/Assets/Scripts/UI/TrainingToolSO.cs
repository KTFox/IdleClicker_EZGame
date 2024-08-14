using UnityEngine;

namespace IdleClicker.UI
{
    [CreateAssetMenu(menuName = "TrainingToolSO")]
    public class TrainingToolSO : ScriptableObject
    {
        // Variables

        [SerializeField] private string toolName;
        [SerializeField] private Sprite icon;
        [SerializeField] private Sprite toolVisual;
        [SerializeField] private float cost;
        [SerializeField] private float earningPerLift;

        // Properties

        public string ToolName => toolName;
        public Sprite Icon => icon;
        public Sprite ToolVisual => toolVisual;
        public float Cost => cost;
        public float EarningPerLift => earningPerLift;

        // Methods
    }
}
