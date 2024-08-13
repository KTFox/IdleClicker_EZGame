using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker
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

        private GameManager gameManager;


        // Methods

        private void Start()
        {
            gameManager = GameManager.Instance;

            upgradeEarningBonusButton.onClick.AddListener(() =>
            {
                gameManager.UpgradeEarningBonus();
            });

            upgradeLiftSpeedButton.onClick.AddListener(() =>
            {
                gameManager.UpgradeLiftSpeed();
            });
        }

        private void Update()
        {
            if (liftSpeed != null)
            {
                liftSpeed.text = gameManager.LiftSpeed.ToString() + "s";
            }

            if (liftSpeedUpgradeCost != null)
            {
                liftSpeedUpgradeCost.text = "$" + gameManager.LiftSpeedUpgradeCost.ToString();
            }

            if (earningBonus != null)
            {
                earningBonus.text = gameManager.EarningBonus.ToString();
            }

            if (earningBonusUpgradeCost != null)
            {
                earningBonusUpgradeCost.text = "$" + gameManager.EarningBonusUpgradeCost.ToString();
            }
        }
    }
}
