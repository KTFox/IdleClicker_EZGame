using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker
{
    public class TrainingToolInfoPanel : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI toolName;
        [SerializeField] private TextMeshProUGUI value;


        // Methods
        public void Setup(TrainingToolSO trainingTool)
        {
            icon.sprite = trainingTool.Icon;
            toolName.text = trainingTool.ToolName;
            value.text = $"Strength gain {trainingTool.EarningPerLift}";
        }
    }
}
