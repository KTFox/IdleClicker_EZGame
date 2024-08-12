using TMPro;
using UnityEngine;

namespace IdleClicker
{
    public class AssetInfoUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI strengthValue;
        [SerializeField] private TextMeshProUGUI moneyValue;

        private GameManager gameManager;

        // Properties


        // Methods

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            strengthValue.text = gameManager.Strength.ToString();
            moneyValue.text = gameManager.Money.ToString();
        }
    }
}
