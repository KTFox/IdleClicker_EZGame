using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.Training
{
    public class TrainingToolHolderUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image icon;
        [SerializeField] private Image cooldownBackground;


        // Methods

        private void OnEnable()
        {
            FindObjectOfType<TrainingToolManager>().OnEquipTrainingTool += UpdateIcon;
        }

        public void RunCooldown(float duration)
        {
            StartCoroutine(RunCooldownCoroutine(duration));
        }

        IEnumerator RunCooldownCoroutine(float duration)
        {
            float timeElapsed = 0f;
            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                cooldownBackground.fillAmount = Mathf.Clamp01(timeElapsed / duration);

                yield return new WaitForEndOfFrame();
            }

            cooldownBackground.fillAmount = 0f;
        }

        public void UpdateIcon(TrainingToolSO trainingTool)
        {
            icon.sprite = trainingTool.Icon;
        }
    }
}
