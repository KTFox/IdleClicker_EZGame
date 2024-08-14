using IdleClicker.Training;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.UI
{
    public class TrainingToolHolderUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image icon;
        [SerializeField] private Image cooldownBackground;

        private TrainingManager gameManager;
        private float timeElapsed;
        private float duration;
        private bool isRunning;


        // Methods

        private void Start()
        {
            gameManager = TrainingManager.Instance;
        }

        private void Update()
        {
            duration = gameManager.TimeInSecondOfTrainingAnimation;

            if (isRunning)
            {
                if (cooldownBackground.fillAmount < 1f)
                {
                    timeElapsed += Time.deltaTime;
                    cooldownBackground.fillAmount = Mathf.Clamp01(timeElapsed / duration);
                }
                else
                {
                    cooldownBackground.fillAmount = 0f;
                    timeElapsed = 0f;
                    isRunning = false;
                }
            }
        }

        public void RunCooldown()
        {
            isRunning = true;
        }

        public void UpdateIcon(TrainingToolSO trainingTool)
        {
            icon.sprite = trainingTool.Icon;
        }
    }
}
