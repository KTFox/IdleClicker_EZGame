using UnityEngine;

namespace IdleClicker.UI
{
    public class BattleGroupUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private BattleSlotUI battleSlotPrefab;
        [SerializeField] private GameObject battleGroupContent;

        private BattleInfoManager battleInfoManager;

        // Properties
        // Methods

        private void Start()
        {
            battleInfoManager = FindObjectOfType<BattleInfoManager>();

            foreach (Transform child in battleGroupContent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (OpponentSO opponent in battleInfoManager.Opponenets)
            {
                BattleSlotUI slot = Instantiate(battleSlotPrefab, battleGroupContent.transform);
                slot.Setup(opponent);
            }
        }
    }
}
