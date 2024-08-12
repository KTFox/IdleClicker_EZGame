using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker
{
    public class TrainingToolForBuyingHolderUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image icon;
        [SerializeField] private Button buyingButton;

        private GameManager gameManager;

        // Properties


        // Methods

        private void OnEnable()
        {
            buyingButton.onClick.AddListener(() =>
            {
                GameManager.Instance.BuyTrainingTool();
            });
        }

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            buyingButton.interactable = gameManager.CanBuyingNewTrainingTool;
        }
    }
}
