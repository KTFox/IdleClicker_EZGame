using IdleClicker.Training;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.UI
{
    public class TrainingToolForBuyingHolderUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image icon;
        [SerializeField] private Button buyingButton;
        [SerializeField] private TextMeshProUGUI buyingCost;

        private TrainingManager gameManager;

        // Properties


        // Methods

        private void OnEnable()
        {
            buyingButton.onClick.AddListener(() =>
            {
                TrainingManager.Instance.BuyTrainingTool();
            });
        }

        private void Start()
        {
            gameManager = TrainingManager.Instance;
        }

        private void Update()
        {
            buyingButton.interactable = gameManager.CanBuyingNewTrainingTool;
        }

        public void UpdateHolderInfo(TrainingToolSO trainingToolSO)
        {
            icon.sprite = gameManager.TrainingToolForBuying.Icon;
            buyingCost.text = "$" + gameManager.TrainingToolForBuying.Cost.ToString();
        }
    }
}
