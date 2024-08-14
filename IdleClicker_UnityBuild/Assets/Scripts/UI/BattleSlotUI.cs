using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.UI
{
    public class BattleSlotUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image avatar;
        [SerializeField] private TextMeshProUGUI opponentName;
        [SerializeField] private TextMeshProUGUI reward;

        // Properties

        // Methods

        public void Setup(OpponentSO opponent)
        {
            avatar.sprite = opponent.Avatar;
            opponentName.text = opponent.Name;
            reward.text = opponent.Reward.ToString();
        }
    }
}
