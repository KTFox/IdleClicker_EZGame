using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.Training
{
    public class UpgradeGroupUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI liftSpeedText;
        [SerializeField] private TextMeshProUGUI liftSpeedUpgradeCostText;
        [SerializeField] private TextMeshProUGUI earningBonusText;
        [SerializeField] private TextMeshProUGUI earningBonusUpgradeCostText;
        [SerializeField] private Button upgradeLiftSpeedButton;
        [SerializeField] private Button upgradeEarningBonusButton;

        private TrainingToolManager trainingToolManager;
        private AssetManager assetManager;


        // Methods

        private void Start()
        {
            trainingToolManager = FindObjectOfType<TrainingToolManager>();
            assetManager = FindObjectOfType<AssetManager>();

            upgradeEarningBonusButton.onClick.AddListener(() =>
            {
                trainingToolManager.UpgradeEarningBonus();
            });

            upgradeLiftSpeedButton.onClick.AddListener(() =>
            {
                trainingToolManager.UpgradeLiftSpeed();
            });
        }

        private void Update()
        {
            upgradeEarningBonusButton.interactable = assetManager.Money >= trainingToolManager.EarningBonusUpgradeCost;
            upgradeLiftSpeedButton.interactable = assetManager.Money >= trainingToolManager.LiftSpeedUpgradeCost;

            liftSpeedText.text = trainingToolManager.TrainingAnimationSpeedInRealTime.ToString("F1") + "s";
            liftSpeedUpgradeCostText.text = "$" + trainingToolManager.LiftSpeedUpgradeCost.ToString("F1");
            earningBonusText.text = trainingToolManager.EarningBonus.ToString();
            earningBonusUpgradeCostText.text = "$" + trainingToolManager.EarningBonusUpgradeCost.ToString("F1");
        }
    }
}
