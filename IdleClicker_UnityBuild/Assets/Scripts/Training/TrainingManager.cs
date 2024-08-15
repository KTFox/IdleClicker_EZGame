using IdleClicker.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IdleClicker.Training
{
    public class TrainingManager : MonoBehaviour
    {
        // Variables

        public static TrainingManager Instance;

        private float ORIGINAL_TIME_OF_CHARACTER_TRAINING_ANIMATION = 1f;
        private const float AUTO_LIFTING_SPEED = 4f;
        private const float MIN_LIFTING_SPEED = 0.1f;
        private const float DECREASING_LIFT_SPEED_PER_LEVEL = 0.1f;
        private const float INCREASING_EARNING_BONUS_PER_LEVEL = 0.2f;
        private const float UPGRADE_COST_MULTIPLIER = 2f;

        [SerializeField] private List<TrainingToolSO> trainingTools = new List<TrainingToolSO>();

        [Header("Training info")]
        [SerializeField] private TrainingToolSO currentTrainingTool;
        [SerializeField] private float liftSpeedUpgradeCost = 10f;
        [SerializeField][Min(1f)] private float earningBonus = 1.0f;
        [SerializeField] private float earningBonusUpgradeCost = 10f;

        [Header("Asset info")]
        [SerializeField] private float strength;
        [SerializeField] private float money;

        [Header("UI")]
        [SerializeField] private TrainingToolHolderUI trainingToolHolderUI;
        [SerializeField] private TrainingToolForBuyingHolderUI trainingToolForBuyingHolderUI;
        [SerializeField] private BagGroupUI bagGroupUI;

        [Header("Character animator")]
        [SerializeField] private Animator characterAnimator;
        [SerializeField] private SpriteRenderer toolSprite;

        private int trainingToolForBuyingIndex;
        private bool canLift = true;
        private float autoLiftTimer;

        // Properties

        public float TimeInSecondOfTrainingAnimation => ORIGINAL_TIME_OF_CHARACTER_TRAINING_ANIMATION / characterAnimator.speed;
        public List<TrainingToolSO> TrainingTools => trainingTools;
        public TrainingToolSO CurrentTrainingTool => currentTrainingTool;
        public float LiftSpeedUpgradeCost => liftSpeedUpgradeCost;
        public float EarningBonus => earningBonus;
        public float EarningBonusUpgradeCost => earningBonusUpgradeCost;
        public float Strength => strength;
        public float Money => money;
        public TrainingToolSO TrainingToolForBuying => trainingTools[trainingToolForBuyingIndex];
        public bool CanBuyingNewTrainingTool => money >= trainingTools[trainingToolForBuyingIndex].Cost;
        public bool CanUpgradeLiftSpeed => money >= LiftSpeedUpgradeCost;
        public bool CanUpgradeEarningBonus => money >= EarningBonusUpgradeCost;
        public float AnimatorSpeed => characterAnimator.speed;
        public float EarningPerLift => currentTrainingTool.EarningPerLift * earningBonus;
        public Sprite ToolVisual => toolSprite.sprite;


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
                trainingToolHolderUI.RunCooldown();
                characterAnimator.GetComponent<CharacterAnimationTrigger>().AnimationTrigger((int)currentTrainingTool.EarningPerLift * earningBonus);
            }
        }

        private void HandleManualLift()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Began)
                {
                    if (canLift)
                    {
                        autoLiftTimer = AUTO_LIFTING_SPEED;
                        canLift = false;
                        trainingToolHolderUI.RunCooldown();
                        characterAnimator.GetComponent<CharacterAnimationTrigger>().AnimationTrigger(currentTrainingTool.EarningPerLift * earningBonus);
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

            if (trainingToolForBuyingIndex >= trainingTools.Count)
            {
                trainingToolForBuyingHolderUI.gameObject.SetActive(false);
            }
            else
            {
                trainingToolForBuyingHolderUI.UpdateHolderInfo(trainingTools[trainingToolForBuyingIndex]);
            }
        }

        public void EquipTrainingTool(TrainingToolSO trainingTool)
        {
            currentTrainingTool = trainingTool;
            toolSprite.sprite = trainingTool.ToolVisual;
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
            if (money < liftSpeedUpgradeCost)
            {
                return;
            }

            if (TimeInSecondOfTrainingAnimation <= MIN_LIFTING_SPEED)
            {
                return;
            }

            money -= liftSpeedUpgradeCost;
            liftSpeedUpgradeCost *= UPGRADE_COST_MULTIPLIER;
            characterAnimator.speed += DECREASING_LIFT_SPEED_PER_LEVEL;
        }

        public void UpgradeEarningBonus()
        {
            if (money < earningBonusUpgradeCost)
            {
                return;
            }

            money -= earningBonusUpgradeCost;
            earningBonusUpgradeCost *= UPGRADE_COST_MULTIPLIER;
            earningBonus += INCREASING_EARNING_BONUS_PER_LEVEL;
        }

        public void GainStrength()
        {
            strength += currentTrainingTool.EarningPerLift * earningBonus;
        }

        public void SetCanLift(bool value)
        {
            canLift = value;
        }
    }
}
