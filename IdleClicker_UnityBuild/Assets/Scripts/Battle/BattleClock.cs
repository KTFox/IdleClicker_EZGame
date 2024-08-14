using IdleClicker.Battle;
using TMPro;
using UnityEngine;

namespace IdleClicker
{
    public class BattleClock : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI timeValue;

        private BattleManager battleManager;


        // Methods

        private void Start()
        {
            battleManager = BattleManager.Instance;
        }

        private void Update()
        {
            timeValue.text = battleManager.CurrentTime.ToString();
        }
    }
}
