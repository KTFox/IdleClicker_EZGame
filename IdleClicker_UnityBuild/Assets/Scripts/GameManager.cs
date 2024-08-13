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

        [SerializeField] private List<TrainingToolSO> trainingTools = new List<TrainingToolSO>();

        [Header("Training info")]
        [SerializeField] private TrainingToolSO currentTrainingTool;
        [SerializeField] private float liftSpeed = 1f;
        [SerializeField] private float[] liftSpeedUpgradeCosts;
        [SerializeField][Min(1f)] private float earningBonus = 1.0f;
        [SerializeField] private float[] earningBonusUpgradeCosts;

        [Header("Asset info")]
        [SerializeField] private float strength;
        [SerializeField] private float money;

        [Header("UI")]
        [SerializeField] private TrainingToolHolderUI trainingToolHolderUI;
        [SerializeField] private TrainingToolForBuyingHolderUI trainingToolForBuyingHolderUI;
        [SerializeField] private BagGroupUI bagGroupUI;

        private int trainingToolForBuyingIndex;
        private int currentLiftSpeedLevel;
        private int currentEarningBonusLevel;
        private float autoLiftTimer;
        private float liftTimer;

        // Properties

        public List<TrainingToolSO> TrainingTools => trainingTools;
        public TrainingToolSO CurrentTrainingTool => currentTrainingTool;
        public float LiftSpeed => liftSpeed;
        public float LiftSpeedUpgradeCost => liftSpeedUpgradeCosts[currentLiftSpeedLevel + 1];
        public float EarningBonus => earningBonus;
        public float EarningBonusUpgradeCost => earningBonusUpgradeCosts[currentEarningBonusLevel + 1];
        public float Strength => strength;
        public float Money => money;
        public TrainingToolSO TrainingToolForBuying => trainingTools[trainingToolForBuyingIndex];
        public float AutoLiftCooldownFraction => autoLiftTimer / AUTO_LIFTING_SPEED;
        public bool CanBuyingNewTrainingTool => money >= trainingTools[trainingToolForBuyingIndex].Cost;


        // Methods

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            BuyTrainingTool();
        }

        private void Update()
        {
            HandleAutoLift();
            HandleManualLift();
        }

        private void HandleAutoLift()
        {
            autoLiftTimer -= Time.deltaTime;
            if (autoLiftTimer <= 0)
            {
                autoLiftTimer = AUTO_LIFTING_SPEED;
                strength += currentTrainingTool.EarningPerLift * earningBonus;
            }
        }

        private void HandleManualLift()
        {
            liftTimer -= Time.deltaTime;

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
        }

        public void BuyTrainingTool()
        {
            if (trainingToolForBuyingIndex > trainingTools.Count - 1)
            {
                Debug.Log("Reached maximum of training tool");
                return;
            }

            if (money < trainingTools[trainingToolForBuyingIndex].Cost)
            {
                Debug.Log("Don't have enough money!!!");
                return;
            }


            money -= trainingTools[trainingToolForBuyingIndex].Cost;
            bagGroupUI.UpdateTrainingToolSlotState(trainingTools[trainingToolForBuyingIndex], 1);
            EquipTrainingTool(trainingTools[trainingToolForBuyingIndex]);

            trainingToolForBuyingIndex++;
            trainingToolForBuyingHolderUI.UpdateHolderInfo(trainingTools[trainingToolForBuyingIndex]);
        }

        public void EquipTrainingTool(TrainingToolSO trainingTool)
        {
            currentTrainingTool = trainingTool;
            trainingToolHolderUI.UpdateIcon(trainingTool);
            bagGroupUI.UpdateTrainingToolSlotState(trainingTool, 2);
        }

        public void ExchangeStrengthForMoney()
        {
            money += strength;
            strength = 0f;
        }

        public void UpgradeLiftSpeed()
        {
            if (money < liftSpeedUpgradeCosts[currentLiftSpeedLevel + 1])
            {
                return;
            }

            if (liftSpeed <= MIN_LIFTING_SPEED)
            {
                return;
            }

            money -= liftSpeedUpgradeCosts[currentLiftSpeedLevel + 1];
            currentLiftSpeedLevel++;
            liftSpeed -= DECREASING_LIFT_SPEED_PER_LEVEL;
        }

        public void UpgradeEarningBonus()
        {
            if (money < earningBonusUpgradeCosts[currentEarningBonusLevel + 1])
            {
                return;
            }

            money -= earningBonusUpgradeCosts[currentEarningBonusLevel + 1];
            currentEarningBonusLevel++;
            earningBonus += INCREASING_EARNING_BONUS_PER_LEVEL;
        }
    }
}
