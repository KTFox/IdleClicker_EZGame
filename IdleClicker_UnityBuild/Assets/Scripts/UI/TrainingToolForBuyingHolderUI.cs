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

        private TrainingManager trainingManager;

        // Properties


        // Methods

        private void OnEnable()
        {
            buyingButton.onClick.AddListener(() =>
            {
                FindObjectOfType<TrainingManager>().BuyTrainingTool();
            });
        }

        private void Start()
        {
            trainingManager = FindObjectOfType<TrainingManager>();
        }

        private void Update()
        {
            buyingButton.interactable = trainingManager.CanBuyingNewTrainingTool;
        }

        public void UpdateHolderInfo(TrainingToolSO trainingToolSO)
        {
            icon.sprite = trainingManager.TrainingToolForBuying.Icon;
            buyingCost.text = "$" + trainingManager.TrainingToolForBuying.Cost.ToString();
        }
    }
}
