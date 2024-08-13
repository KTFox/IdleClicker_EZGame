using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker
{
    public class TrainingToolHolderUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image icon;
        [SerializeField] private Image cooldownBackground;

        private GameManager gameManager;


        // Methods

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            cooldownBackground.fillAmount = gameManager.AutoLiftCooldownFraction;
        }

        public void UpdateIcon(TrainingToolSO trainingTool)
        {
            icon.sprite = trainingTool.Icon;
        }
    }
}
