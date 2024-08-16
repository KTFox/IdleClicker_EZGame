using TMPro;
using UnityEngine;

namespace IdleClicker.Battle
{
    public class BattleClock : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI timeValue;

        private BattleManager battleManager;


        // Methods

        private void Start()
        {
            battleManager = FindObjectOfType<BattleManager>();
        }

        private void Update()
        {
            timeValue.text = battleManager.CurrentTime.ToString();
        }
    }
}
