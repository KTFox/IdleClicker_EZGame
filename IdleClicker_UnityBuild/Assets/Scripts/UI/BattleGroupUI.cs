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

        private void OnEnable()
        {
            FindObjectOfType<BattleInfoManager>().OnOpponentConfigUpdate += BattleInfoManager_OnOpponentConfigUpdate;
        }

        private void Start()
        {
            battleInfoManager = FindObjectOfType<BattleInfoManager>();

            BattleInfoManager_OnOpponentConfigUpdate();
        }

        private void BattleInfoManager_OnOpponentConfigUpdate()
        {
            foreach (Transform child in battleGroupContent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (BattleInfoManager.OpponentConfig config in battleInfoManager.OpponentConfigs)
            {
                BattleSlotUI slot = Instantiate(battleSlotPrefab, battleGroupContent.transform);
                slot.Setup(config.Opponent);
                slot.SetUnlocked(config.IsUnlocked);
            }
        }
    }
}
