using IdleClicker.Training;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.UI
{
    public class TrainingToolInfoPanel : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI toolName;
        [SerializeField] private TextMeshProUGUI value;
        [SerializeField] private Button equipButton;
        [SerializeField] private TextMeshProUGUI equipButtonText;

        private TrainingToolSO currentSelectedTool;


        // Methods

        private void Start()
        {
            equipButton.onClick.AddListener(() =>
            {
                FindObjectOfType<TrainingManager>().EquipTrainingTool(currentSelectedTool);
            });
        }

        public void Setup(TrainingToolSO trainingTool)
        {
            currentSelectedTool = trainingTool;
            icon.sprite = trainingTool.Icon;
            toolName.text = trainingTool.ToolName;
            value.text = $"Strength gain {trainingTool.EarningPerLift}";

            if (FindObjectOfType<TrainingManager>().CurrentTrainingTool == trainingTool)
            {
                equipButton.interactable = false;
                equipButtonText.text = "Equipped";
            }
            else
            {
                equipButton.interactable = true;
                equipButtonText.text = "Equip";
            }
        }
    }
}
