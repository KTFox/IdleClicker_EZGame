using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleClicker
{
    public class TrainingToolManager : MonoBehaviour
    {
        // Variables

        private const float ORIGINAL_REAL_TIME_TRAING_ANIMATOR_SPEED = 1f;
        private const float MIN_REAL_TIME_TRAINING_SPEED = 0.1f;
        private const float MAX_LIFTING_SPEED = ORIGINAL_REAL_TIME_TRAING_ANIMATOR_SPEED / MIN_REAL_TIME_TRAINING_SPEED;

        private const float INCREASING_LIFT_SPEED_PER_LEVEL = 0.1f;
        private const float INCREASING_EARNING_BONUS_PER_LEVEL = 0.2f;
        private const float UPGRADE_COST_MULTIPLIER = 2f;

        [SerializeField] private List<TrainingToolSO> trainingTools = new List<TrainingToolSO>();
        private TrainingToolConfig[] trainingToolConfigs;

        private AssetManager assetManager;

        private TrainingToolSO currentTraingTool;

        private float liftSpeed = 1f;
        private float liftSpeedUpgradeCost = 10f;

        private float earningBonus = 1f;
        private float earningBonusUpgradeCost = 10f;

        // Properties

        public TrainingToolConfig[] TrainingToolConfigs => trainingToolConfigs;
        public TrainingToolSO CurrentTrainingTool => currentTraingTool;
        public float LiftSpeed => liftSpeed;
        public float LiftSpeedUpgradeCost => liftSpeedUpgradeCost;
        public float EarningBonusUpgradeCost => earningBonusUpgradeCost;
        public float TrainingAnimationSpeedInRealTime => ORIGINAL_REAL_TIME_TRAING_ANIMATOR_SPEED / liftSpeed;
        public float EarningBonus => earningBonus;
        public float EarningPerLift => currentTraingTool.EarningPerLift * earningBonus;

        // Structs

        public class TrainingToolConfig
        {
            public TrainingToolSO TrainingTool;
            public bool HasBought;
        }

        // Events

        public event Action OnTraingToolManagerUpdate;
        public event Action<TrainingToolSO> OnEquipTrainingTool;


        // Methods

        private void Awake()
        {
            trainingToolConfigs = new TrainingToolConfig[trainingTools.Count];
            for (int i = 0; i < trainingTools.Count; i++)
            {
                TrainingToolConfig trainingToolConfig = new TrainingToolConfig();
                trainingToolConfig.TrainingTool = trainingTools[i];
                trainingToolConfig.HasBought = false;

                trainingToolConfigs[i] = trainingToolConfig;
            }
        }

        private void Start()
        {
            assetManager = FindObjectOfType<AssetManager>();

            BuyTrainingTool(trainingTools[0]);
        }

        public void BuyTrainingTool(TrainingToolSO trainingTool)
        {
            foreach (TrainingToolConfig trainingToolConfig in trainingToolConfigs)
            {
                if (trainingToolConfig.TrainingTool == trainingTool)
                {
                    if (assetManager.Money < trainingTool.Cost)
                    {
                        return;
                    }

                    assetManager.ChangeMoneyValue(-trainingTool.Cost);
                    trainingToolConfig.HasBought = true;
                    OnTraingToolManagerUpdate?.Invoke();
                    EquipTrainingTool(trainingTool);
                }
            }
        }

        public void EquipTrainingTool(TrainingToolSO trainingTool)
        {
            foreach (TrainingToolConfig trainingToolConfig in trainingToolConfigs)
            {
                if (trainingToolConfig.TrainingTool == trainingTool)
                {
                    if (!trainingToolConfig.HasBought)
                    {
                        return;
                    }

                    currentTraingTool = trainingTool;
                    OnEquipTrainingTool?.Invoke(trainingTool);
                }
            }
        }

        public void UpgradeLiftSpeed()
        {
            if (assetManager.Money < liftSpeedUpgradeCost)
            {
                return;
            }

            if (liftSpeed >= MAX_LIFTING_SPEED)
            {
                return;
            }

            assetManager.ChangeMoneyValue(-liftSpeedUpgradeCost);
            liftSpeed += INCREASING_LIFT_SPEED_PER_LEVEL;
            liftSpeedUpgradeCost *= UPGRADE_COST_MULTIPLIER;
        }

        public void UpgradeEarningBonus()
        {
            if (assetManager.Money < earningBonusUpgradeCost)
            {
                return;
            }

            assetManager.ChangeMoneyValue(-earningBonusUpgradeCost);
            earningBonusUpgradeCost *= UPGRADE_COST_MULTIPLIER;
            earningBonus += INCREASING_EARNING_BONUS_PER_LEVEL;
        }
    }
}
