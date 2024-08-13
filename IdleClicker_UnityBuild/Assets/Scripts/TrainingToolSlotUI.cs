using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker
{
    public class TrainingToolSlotUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private Image lockIcon;
        [SerializeField] private Image icon;
        [SerializeField] private Image selectedIcon;

        private TrainingToolSO trainingTool;
        private int state;

        // Properties

        public TrainingToolSO TrainingTool => trainingTool;
        public int State => state;


        // Methods

        public void Setup(TrainingToolSO trainingTool)
        {
            icon.sprite = trainingTool.Icon;
            this.trainingTool = trainingTool;
            ChangeState(0);
        }

        /// <summary>
        /// 0 is locked state,
        /// 1 is unSelected state,
        /// 2 is selected state.
        /// </summary>
        /// <param name="stateIndex"></param>
        public void ChangeState(int stateIndex)
        {
            Button button = GetComponent<Button>();

            if (stateIndex == 0)
            {
                lockIcon.gameObject.SetActive(true);
                icon.gameObject.SetActive(false);
                selectedIcon.gameObject.SetActive(false);
                button.interactable = false;
            }
            else if (stateIndex == 1)
            {
                lockIcon.gameObject.SetActive(false);
                icon.gameObject.SetActive(true);
                selectedIcon.gameObject.SetActive(false);
                button.interactable = true;
            }
            else if (stateIndex == 2)
            {
                lockIcon.gameObject.SetActive(false);
                icon.gameObject.SetActive(true);
                selectedIcon.gameObject.SetActive(true);
                button.interactable = true;
            }
            else
            {
                Debug.LogError("State index is not exist");
            }

            state = stateIndex;
        }
    }
}
