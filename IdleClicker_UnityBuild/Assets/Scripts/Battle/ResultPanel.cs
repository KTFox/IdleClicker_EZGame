using UnityEngine;

namespace IdleClicker
{
    public class ResultPanel : MonoBehaviour
    {
        // Variables

        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;


        // Methods

        private void Start()
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }

        public void TurnOnResultPanel(bool isWin)
        {
            if (isWin)
            {
                winPanel.SetActive(true);
            }
            else
            {
                losePanel.SetActive(true);
            }
        }
    }
}
