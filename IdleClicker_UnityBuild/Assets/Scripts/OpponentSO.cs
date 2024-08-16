using UnityEditor.Animations;
using UnityEngine;

namespace IdleClicker
{
    [CreateAssetMenu(menuName = "Fighter")]
    public class OpponentSO : ScriptableObject
    {
        // Variables

        [SerializeField] private string opponentName;
        [SerializeField] private Sprite avatar;
        [SerializeField] private float reward;
        [SerializeField] private TrainingToolSO trainingTool;
        [SerializeField] private AnimatorController animatorController;
        [SerializeField] private float liftSpeed;

        // Properties

        public string Name => opponentName;
        public Sprite Avatar => avatar;
        public float Reward => reward;
        public TrainingToolSO TrainingTool => trainingTool;
        public AnimatorController AnimatorController => animatorController;
        public float LiftSpeed => liftSpeed;

        // Methods
    }
}
