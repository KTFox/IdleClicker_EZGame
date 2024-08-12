using System.Collections.Generic;
using UnityEngine;

namespace IdleClicker
{
    public class GameManager : MonoBehaviour
    {
        // Variables

        private const float AUTO_LIFTING_SPEED = 2f;

        [Header("Training info")]
        [SerializeField] private TrainingToolSO currentTrainingTool;
        [SerializeField] private float lifttSpeed;
        [SerializeField][Min(1f)] private float earningBonus = 1.5f;
        [SerializeField] private List<TrainingToolSO> trainingTools = new List<TrainingToolSO>();

        [Header("Asset info")]
        [SerializeField] private float strength;
        [SerializeField] private float money;

        private int trainingToolForBuyingIndex;
        private float autoLiftTimer;

        // Properties


        // Methods

        private void Start()
        {
            currentTrainingTool = trainingTools[0];
            trainingToolForBuyingIndex = 1;
        }

        private void Update()
        {
            autoLiftTimer -= Time.deltaTime;
            if (autoLiftTimer <= 0)
            {
                autoLiftTimer = AUTO_LIFTING_SPEED;
                strength += currentTrainingTool.EarningPerLift * earningBonus;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExchangeStrengthForMoney();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                BuyTrainingTool();
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

            return true;
        }

        public void EquipTrainingTool(TrainingToolSO trainingTool)
        {
            currentTrainingTool = trainingTool;
        }
    }
}
