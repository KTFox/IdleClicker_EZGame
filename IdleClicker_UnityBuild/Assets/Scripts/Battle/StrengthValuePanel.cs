using TMPro;
using UnityEngine;

namespace IdleClicker.Battle
{
    public class StrengthValuePanel : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI playerStrengthValue;
        [SerializeField] private TextMeshProUGUI opponentStrengthValue;

        private BattleManager battleManager;

        // Methods

        private void Start()
        {
            battleManager = FindObjectOfType<BattleManager>();
        }

        private void Update()
        {
            playerStrengthValue.text = ((int)battleManager.PlayerCurrentStrength).ToString();
            opponentStrengthValue.text = ((int)battleManager.OpponentCurrentStrength).ToString();
        }
    }
}
