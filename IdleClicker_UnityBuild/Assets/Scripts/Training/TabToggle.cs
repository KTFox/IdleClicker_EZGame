using UnityEngine;

namespace IdleClicker.Training
{
    public class TabToggle : MonoBehaviour
    {
        // Variables

        [SerializeField] private GameObject upgradeTab;
        [SerializeField] private GameObject bagTab;
        [SerializeField] private GameObject battleTab;
        [SerializeField] private GameObject settingTab;


        // Methods

        private void Start()
        {
            upgradeTab.SetActive(false);
            bagTab.SetActive(false);
            battleTab.SetActive(false);
            settingTab.SetActive(false);
        }

        public void ToggleUpgradeTab()
        {
            upgradeTab.SetActive(!upgradeTab.activeSelf);
        }

        public void ToggleBagTab()
        {
            bagTab.SetActive(!bagTab.activeSelf);
        }

        public void ToggleBattleTab()
        {
            battleTab.SetActive(!battleTab.activeSelf);
        }

        public void ToggleSettingTab()
        {
            settingTab.SetActive(!settingTab.activeSelf);
        }
    }
}
