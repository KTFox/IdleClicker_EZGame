using TMPro;
using UnityEngine;

namespace IdleClicker
{
    public class ResultPanel : MonoBehaviour
    {
        // Variables

        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private TextMeshProUGUI rewardValue;


        // Methods

        private void Start()
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }

        public void TurnOnResultPanel(bool isWin, float reward = 0f)
        {
            if (isWin)
            {
                winPanel.SetActive(true);
                rewardValue.text = $"+{reward}";
            }
            else
            {
                losePanel.SetActive(true);
            }
        }
    }
}
