using UnityEngine;

namespace IdleClicker.Training
{
    public class TabToggle : MonoBehaviour
    {
        // Variables

        [SerializeField] private GameObject upgradeTab;
        [SerializeField] private GameObject bagTab;
        [SerializeField] private GameObject battleTab;


        // Methods

        private void Start()
        {
            upgradeTab.SetActive(false);
            bagTab.SetActive(false);
            battleTab.SetActive(false);
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
    }
}
