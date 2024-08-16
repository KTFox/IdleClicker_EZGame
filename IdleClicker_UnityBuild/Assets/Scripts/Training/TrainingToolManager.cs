using IdleClicker.Saving;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleClicker.Training
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

        [System.Serializable]
        public class TrainingToolConfig
        {
            public TrainingToolSO TrainingTool;
            public bool HasBought;
        }

        // Events

        public event Action OnTraingToolManagerUpdate;
        public event Action<TrainingToolSO> OnEquipTrainingTool;


        // Methods

        private void Start()
        {
            assetManager = FindObjectOfType<AssetManager>();

            PersistenceData persistenceData = Resources.Load<PersistenceData>("PersistenceData");
            if (persistenceData == null)
            {
                Debug.LogError("Persistence data is not found");
            }

            if (persistenceData.TrainingToolConfigs.Length == 0)
            {
                trainingToolConfigs = new TrainingToolConfig[trainingTools.Count];
                for (int i = 0; i < trainingTools.Count; i++)
                {
                    TrainingToolConfig trainingToolConfig = new TrainingToolConfig();
                    trainingToolConfig.TrainingTool = trainingTools[i];
                    trainingToolConfig.HasBought = false;

                    trainingToolConfigs[i] = trainingToolConfig;
                }

                Debug.Log("TrainingToolManager: instantiate new trainingToolConfigs");
            }
            else
            {
                trainingToolConfigs = persistenceData.TrainingToolConfigs;
            }

            if (persistenceData.CurrentTrainingTool == null)
            {
                BuyTrainingTool(trainingTools[0]);
                FindObjectOfType<TrainingToolInfoPanel>().Setup(currentTraingTool);
            }
            else
            {
                EquipTrainingTool(persistenceData.CurrentTrainingTool);
                FindObjectOfType<TrainingToolInfoPanel>().Setup(currentTraingTool);
                OnTraingToolManagerUpdate?.Invoke();
            }

            if (persistenceData.LiftSpeed <= 0)
            {
                liftSpeed = 1;
            }
            else
            {
                liftSpeed = persistenceData.LiftSpeed;
            }

            if (persistenceData.LiftSpeedUpgradeCost <= 0)
            {
                liftSpeedUpgradeCost = 10f;
            }
            else
            {
                liftSpeedUpgradeCost = persistenceData.LiftSpeedUpgradeCost;
            }

            if (persistenceData.EarningBonus <= 0)
            {
                earningBonus = 1;
            }
            else
            {
                earningBonus = persistenceData.EarningBonus;
            }

            if (persistenceData.EarningBonusUpgradeCost <= 0)
            {
                earningBonusUpgradeCost = 10f;
            }
            else
            {
                earningBonusUpgradeCost = persistenceData.EarningBonusUpgradeCost;
            }
        }

        private void OnDisable()
        {
            PersistenceData persistenceData = Resources.Load<PersistenceData>("PersistenceData");
            if (persistenceData == null)
            {
                Debug.LogError("Persistence data is not found");
            }

            persistenceData.TrainingToolConfigs = trainingToolConfigs;
            persistenceData.CurrentTrainingTool = currentTraingTool;
            persistenceData.LiftSpeed = liftSpeed;
            persistenceData.LiftSpeedUpgradeCost = liftSpeedUpgradeCost;
            persistenceData.EarningBonus = earningBonus;
            persistenceData.EarningBonusUpgradeCost = earningBonusUpgradeCost;
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
