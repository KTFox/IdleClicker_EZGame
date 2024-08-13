using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IdleClicker
{
    public class GameManager : MonoBehaviour
    {
        // Variables

        public static GameManager Instance;

        private const float AUTO_LIFTING_SPEED = 2f;
        private const float MIN_LIFTING_SPEED = 0.1f;
        private const float DECREASING_LIFT_SPEED_PER_LEVEL = 0.1f;
        private const float INCREASING_EARNING_BONUS_PER_LEVEL = 0.2f;

        [Header("Training info")]
        [SerializeField] private TrainingToolSO currentTrainingTool;
        [SerializeField] private float liftSpeed = 1f;
        [SerializeField] private float[] liftSpeedUpgradeCosts;
        [SerializeField][Min(1f)] private float earningBonus = 1.0f;
        [SerializeField] private float[] earningBonusUpgradeCosts;
        [SerializeField] private List<TrainingToolSO> trainingTools = new List<TrainingToolSO>();

        [Header("Asset info")]
        [SerializeField] private float strength;
        [SerializeField] private float money;

        private int trainingToolForBuyingIndex;
        private int currentLiftSpeedLevel;
        private int currentEarningBonusLevel;
        private float autoLiftTimer;
        private float liftTimer;

        // Properties

        public float Strength => strength;
        public float Money => money;
        public TrainingToolSO CurrentTrainingTool => currentTrainingTool;
        public TrainingToolSO TrainingToolForBuying => trainingTools[trainingToolForBuyingIndex];
        public float LiftSpeed => liftSpeed;
        public float LiftSpeedUpgradeCost => liftSpeedUpgradeCosts[currentLiftSpeedLevel + 1];
        public float EarningBonus => earningBonus;
        public float EarningBonusUpgradeCost => earningBonusUpgradeCosts[currentEarningBonusLevel + 1];
        public float CooldownFraction => autoLiftTimer / AUTO_LIFTING_SPEED;
        public bool CanBuyingNewTrainingTool => money >= trainingTools[trainingToolForBuyingIndex].Cost;

        // Events

        public event Action OnBuyingTrainingTool;
        public event Action OnEquipTrainingTool;


        // Methods

        private void Awake()
        {
            Instance = this;

            BuyTrainingTool();
        }

        private void Update()
        {
            autoLiftTimer -= Time.deltaTime;
            liftTimer -= Time.deltaTime;

            if (autoLiftTimer <= 0)
            {
                autoLiftTimer = AUTO_LIFTING_SPEED;
                strength += currentTrainingTool.EarningPerLift * earningBonus;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Began)
                {
                    if (liftTimer <= 0)
                    {
                        liftTimer = liftSpeed;
                        autoLiftTimer = AUTO_LIFTING_SPEED;
                        strength += currentTrainingTool.EarningPerLift * earningBonus;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                UpgradeLiftSpeed();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                UpgradeEarningBonus();
            }
        }

        public void ExchangeStrengthForMoney()
        {
            money += strength;
            strength = 0f;
        }

        public bool BuyTrainingTool()
        {
            if (trainingToolForBuyingIndex > trainingTools.Count - 1)
            {
                Debug.Log("Reached maximum of training tool");
                return false;
            }

            if (money < trainingTools[trainingToolForBuyingIndex].Cost)
            {
                Debug.Log("Don't have enough money!!!");
                return false;
            }

            EquipTrainingTool(trainingTools[trainingToolForBuyingIndex]);
            money -= trainingTools[trainingToolForBuyingIndex].Cost;
            trainingToolForBuyingIndex++;

            OnBuyingTrainingTool?.Invoke();

            return true;
        }

        public void EquipTrainingTool(TrainingToolSO trainingTool)
        {
            currentTrainingTool = trainingTool;

            OnEquipTrainingTool?.Invoke();
        }

        public bool UpgradeLiftSpeed()
        {
            if (money < liftSpeedUpgradeCosts[currentLiftSpeedLevel + 1])
            {
                return false;
            }

            if (liftSpeed <= MIN_LIFTING_SPEED)
            {
                return false;
            }

            money -= liftSpeedUpgradeCosts[currentLiftSpeedLevel + 1];
            currentLiftSpeedLevel++;
            liftSpeed -= DECREASING_LIFT_SPEED_PER_LEVEL;

            return true;
        }

        public bool UpgradeEarningBonus()
        {
            if (money < earningBonusUpgradeCosts[currentEarningBonusLevel + 1])
            {
                return false;
            }

            money -= earningBonusUpgradeCosts[currentEarningBonusLevel + 1];
            currentEarningBonusLevel++;
            earningBonus += INCREASING_EARNING_BONUS_PER_LEVEL;

            return true;
        }
    }
}
