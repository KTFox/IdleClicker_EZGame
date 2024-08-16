using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.Training
{
    public class TrainingToolForBuyingHolderUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image icon;
        [SerializeField] private Button buyingButton;
        [SerializeField] private TextMeshProUGUI buyingCost;

        private AssetManager assetManager;

        private TrainingToolSO trainingTool;

        // Properties


        // Methods

        private void OnEnable()
        {
            FindObjectOfType<TrainingToolManager>().OnTraingToolManagerUpdate += TrainingToolForBuyingHolderUI_OnTraingToolManagerUpdate;

            buyingButton.onClick.AddListener(() =>
            {
                FindObjectOfType<TrainingToolManager>().BuyTrainingTool(this.trainingTool);
            });
        }

        private void Start()
        {
            assetManager = FindObjectOfType<AssetManager>();
        }

        private void Update()
        {
            buyingButton.interactable = assetManager.Money >= this.trainingTool.Cost;
        }

        private void TrainingToolForBuyingHolderUI_OnTraingToolManagerUpdate()
        {
            TrainingToolManager.TrainingToolConfig[] trainingToolConfigs = FindObjectOfType<TrainingToolManager>().TrainingToolConfigs;

            for (int i = 1; i < trainingToolConfigs.Length; i++)
            {
                if (!trainingToolConfigs[i].HasBought)
                {
                    this.trainingTool = trainingToolConfigs[i].TrainingTool;
                    SetHolderUI(this.trainingTool);
                    return;
                }
            }
        }

        private void SetHolderUI(TrainingToolSO trainingTool)
        {
            icon.sprite = trainingTool.Icon;
            buyingCost.text = $"${trainingTool.Cost}";
        }
    }
}
