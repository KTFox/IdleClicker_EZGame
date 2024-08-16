using UnityEngine;

namespace IdleClicker.Training
{
    public class BattleGroupUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private BattleSlotUI battleSlotPrefab;
        [SerializeField] private GameObject battleGroupContent;

        // Properties
        // Methods

        private void OnEnable()
        {
            FindObjectOfType<BattleInfoManager>().OnOpponentConfigUpdate += BattleInfoManager_OnOpponentConfigUpdate;
        }

        private void Start()
        {
            BattleInfoManager_OnOpponentConfigUpdate();
        }

        private void BattleInfoManager_OnOpponentConfigUpdate()
        {
            foreach (Transform child in battleGroupContent.transform)
            {
                Destroy(child.gameObject);
            }

            BattleInfoManager battleInfoManager = FindObjectOfType<BattleInfoManager>();
            foreach (BattleInfoManager.OpponentConfig config in battleInfoManager.OpponentConfigs)
            {
                BattleSlotUI slot = Instantiate(battleSlotPrefab, battleGroupContent.transform);
                slot.Setup(config.Opponent);
                slot.SetUnlocked(config.IsUnlocked);
            }
        }
    }
}
