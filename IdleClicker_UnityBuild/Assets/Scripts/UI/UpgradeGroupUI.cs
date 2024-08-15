using IdleClicker.Training;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.UI
{
    public class UpgradeGroupUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI liftSpeed;
        [SerializeField] private TextMeshProUGUI liftSpeedUpgradeCost;
        [SerializeField] private TextMeshProUGUI earningBonus;
        [SerializeField] private TextMeshProUGUI earningBonusUpgradeCost;
        [SerializeField] private Button upgradeLiftSpeedButton;
        [SerializeField] private Button upgradeEarningBonusButton;

        private TrainingManager trainingManager;


        // Methods

        private void Start()
        {
            trainingManager = FindObjectOfType<TrainingManager>();

            upgradeEarningBonusButton.onClick.AddListener(() =>
            {
                trainingManager.UpgradeEarningBonus();
            });

            upgradeLiftSpeedButton.onClick.AddListener(() =>
            {
                trainingManager.UpgradeLiftSpeed();
            });
        }

        private void Update()
        {
            upgradeEarningBonusButton.interactable = trainingManager.CanUpgradeEarningBonus;
            upgradeLiftSpeedButton.interactable = trainingManager.CanUpgradeLiftSpeed;

            if (liftSpeed != null)
            {
                liftSpeed.text = trainingManager.TimeInSecondOfTrainingAnimation.ToString("F1") + "s";
            }

            if (liftSpeedUpgradeCost != null)
            {
                liftSpeedUpgradeCost.text = "$" + trainingManager.LiftSpeedUpgradeCost.ToString("F1");
            }

            if (earningBonus != null)
            {
                earningBonus.text = trainingManager.EarningBonus.ToString();
            }

            if (earningBonusUpgradeCost != null)
            {
                earningBonusUpgradeCost.text = "$" + trainingManager.EarningBonusUpgradeCost.ToString("F1");
            }
        }
    }
}
