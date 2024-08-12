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

        [Header("Training info")]
        [SerializeField] private TrainingToolSO currentTrainingTool;
        [SerializeField] private float lifttSpeed = 1f;
        [SerializeField][Min(1f)] private float earningBonus = 1.5f;
        [SerializeField] private List<TrainingToolSO> trainingTools = new List<TrainingToolSO>();

        [Header("Asset info")]
        [SerializeField] private float strength;
        [SerializeField] private float money;

        private int trainingToolForBuyingIndex;
        private float autoLiftTimer;
        private float liftTimer;

        // Properties

        public float Strength => strength;
        public float Money => money;
        public TrainingToolSO CurrentTrainingTool => currentTrainingTool;
        public TrainingToolSO TrainingToolForBuying => trainingTools[trainingToolForBuyingIndex];
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
                        liftTimer = lifttSpeed;
                        autoLiftTimer = AUTO_LIFTING_SPEED;
                        strength += currentTrainingTool.EarningPerLift * earningBonus;
                    }
                }
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
    }
}
