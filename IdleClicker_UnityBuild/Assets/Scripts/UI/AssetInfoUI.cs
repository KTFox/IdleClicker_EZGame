using IdleClicker.Training;
using TMPro;
using UnityEngine;

namespace IdleClicker.UI
{
    public class AssetInfoUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI strengthValue;
        [SerializeField] private TextMeshProUGUI moneyValue;

        private TrainingManager trainingManager;

        // Properties


        // Methods

        private void Start()
        {
            trainingManager = FindObjectOfType<TrainingManager>();
        }

        private void Update()
        {
            strengthValue.text = ((int)trainingManager.Strength).ToString();
            moneyValue.text = ((int)trainingManager.Money).ToString();
        }
    }
}
