using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IdleClicker.UI
{
    public class BattleSlotUI : MonoBehaviour
    {
        // Variables

        [Header("Unlock content")]
        [SerializeField] private GameObject unlockedContent;
        [SerializeField] private Image avatar;
        [SerializeField] private TextMeshProUGUI opponentName;
        [SerializeField] private TextMeshProUGUI reward;
        [SerializeField] private Button fightButton;

        [Header("Locked content")]
        [SerializeField] private GameObject lockedContent;

        private OpponentSO opponent;


        // Methods

        private void Awake()
        {
            fightButton.onClick.AddListener(() =>
            {
                FindObjectOfType<BattleInfoManager>().SetFighter(this.opponent);
                SceneManager.LoadScene("Battle");
            });
        }

        public void Setup(OpponentSO opponent)
        {
            this.opponent = opponent;
            avatar.sprite = opponent.Avatar;
            opponentName.text = opponent.Name;
            reward.text = opponent.Reward.ToString();
        }

        public void SetUnlocked(bool value)
        {
            if (value == true)
            {
                unlockedContent.SetActive(true);
                lockedContent.SetActive(false);
            }
            else
            {
                unlockedContent.SetActive(false);
                lockedContent.SetActive(true);
            }
        }
    }
}
