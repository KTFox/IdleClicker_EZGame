using UnityEngine;

namespace IdleClicker.UI
{
    public class TabToggle : MonoBehaviour
    {
        // Variables

        [SerializeField] private GameObject upgradeTab;
        [SerializeField] private GameObject bagTab;


        // Methods

        private void Start()
        {
            upgradeTab.SetActive(false);
            bagTab.SetActive(false);
        }

        public void ToggleUpgradeTab()
        {
            upgradeTab.SetActive(!upgradeTab.activeSelf);
        }

        public void ToggleBagTab()
        {
            bagTab.SetActive(!bagTab.activeSelf);
        }
    }
}
