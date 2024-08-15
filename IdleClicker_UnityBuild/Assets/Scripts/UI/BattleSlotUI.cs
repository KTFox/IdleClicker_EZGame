using IdleClicker.Training;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IdleClicker.UI
{
    public class BattleSlotUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image avatar;
        [SerializeField] private TextMeshProUGUI opponentName;
        [SerializeField] private TextMeshProUGUI reward;
        [SerializeField] private Button fightButton;

        private OpponentSO opponent;

        // Properties

        // Methods

        private void Awake()
        {
            fightButton.onClick.AddListener(() =>
            {
                var persistenceData = Resources.Load<PersistenceData>("PersistenceData");
                if (persistenceData == null)
                {
                    Debug.LogError("Persistence data is not found");
                    return;
                }

                TrainingManager trainingManager = FindObjectOfType<TrainingManager>();

                persistenceData.playerAnimatorSpeed = trainingManager.AnimatorSpeed;
                persistenceData.playerEarningPerLift = trainingManager.EarningPerLift;
                persistenceData.playerToolVisual = trainingManager.ToolVisual;
                persistenceData.opponentSO = this.opponent;

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
    }
}
