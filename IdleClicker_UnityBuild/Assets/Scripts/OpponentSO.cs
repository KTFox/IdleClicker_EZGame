using UnityEngine;

namespace IdleClicker
{
    [CreateAssetMenu(menuName = "OpponentSO")]
    public class OpponentSO : ScriptableObject
    {
        // Variables

        [SerializeField] private string opponentName;
        [SerializeField] private Sprite avatar;
        [SerializeField] private float reward;
        [SerializeField] private TrainingToolSO trainingTool;
        [SerializeField] private Animator animator;
        [SerializeField] private float animatorSpeed;

        // Properties

        public string Name => opponentName;
        public Sprite Avatar => avatar;
        public float Reward => reward;
        public TrainingToolSO TrainingTool => trainingTool;
        public Animator Animator => animator;
        public float AnimatorSpeed => animatorSpeed;

        // Methods
    }
}
