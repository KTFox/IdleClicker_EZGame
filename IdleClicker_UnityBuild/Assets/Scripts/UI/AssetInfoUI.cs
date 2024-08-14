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

        private TrainingManager gameManager;

        // Properties


        // Methods

        private void Start()
        {
            gameManager = TrainingManager.Instance;
        }

        private void Update()
        {
            strengthValue.text = ((int)gameManager.Strength).ToString();
            moneyValue.text = ((int)gameManager.Money).ToString();
        }
    }
}
