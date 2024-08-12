using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker
{
    public class TrainingToolHolderUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image Icon;
        [SerializeField] private Image cooldownBackground;

        private GameManager gameManager;

        // Properties


        // Methods

        private void Start()
        {
            gameManager = GameManager.Instance;

            gameManager.OnEquipTrainingTool += GameManager_OnEquipTrainingTool;

            GameManager_OnEquipTrainingTool();
        }

        private void GameManager_OnEquipTrainingTool()
        {
            Icon.sprite = gameManager.CurrentTrainingTool.Icon;
        }

        private void Update()
        {
            cooldownBackground.fillAmount = gameManager.CooldownFraction;
        }
    }
}
