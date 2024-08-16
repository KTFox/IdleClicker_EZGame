using IdleClicker.Training;
using UnityEngine;

namespace IdleClicker.Saving
{
    [CreateAssetMenu(menuName = "PersistenceData")]
    public class PersistenceData : ScriptableObject
    {
        [Header("AssetManager data")]
        public float Strength;
        public float Money;

        [Header("TrainingToolManager data")]
        public TrainingToolManager.TrainingToolConfig[] TrainingToolConfigs;
        public TrainingToolSO CurrentTrainingTool;
        public float LiftSpeed;
        public float LiftSpeedUpgradeCost;
        public float EarningBonus;
        public float EarningBonusUpgradeCost;

        [Header("BattleInfoManager data")]
        public BattleInfoManager.OpponentConfig[] OpponentConfigs;

        [Header("Opponent info")]
        public OpponentSO Fighter;
    }
}
