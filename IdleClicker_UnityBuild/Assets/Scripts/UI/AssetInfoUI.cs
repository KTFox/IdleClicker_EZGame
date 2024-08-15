using TMPro;
using UnityEngine;

namespace IdleClicker.UI
{
    public class AssetInfoUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI strengthValue;
        [SerializeField] private TextMeshProUGUI moneyValue;

        private AssetManager assetManager;


        // Methods

        private void Start()
        {
            assetManager = FindObjectOfType<AssetManager>();
        }

        private void Update()
        {
            strengthValue.text = ((int)assetManager.Strength).ToString();
            moneyValue.text = ((int)assetManager.Money).ToString();
        }
    }
}
