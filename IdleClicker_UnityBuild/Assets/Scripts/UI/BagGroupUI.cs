using IdleClicker.Training;
using UnityEngine;

namespace IdleClicker.UI
{
    public class BagGroupUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private GameObject bagContent;
        [SerializeField] private TrainingToolSlotUI trainingToolSlotPrefab;
        [SerializeField] private TrainingToolInfoPanel trainingToolInfoPanel;


        // Methods

        private void Awake()
        {
            foreach (Transform child in bagContent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (TrainingToolSO trainingTool in FindObjectOfType<TrainingManager>().TrainingTools)
            {
                TrainingToolSlotUI trainingToolSlot = Instantiate(trainingToolSlotPrefab, bagContent.transform);
                trainingToolSlot.Setup(trainingTool);
            }
        }

        public void UpdateTrainingToolSlotState(TrainingToolSO trainingTool, int state)
        {
            TrainingToolSlotUI[] trainingToolSlots = bagContent.GetComponentsInChildren<TrainingToolSlotUI>();

            if (state == 2)
            {
                foreach (TrainingToolSlotUI trainingToolSlot in trainingToolSlots)
                {
                    if (trainingToolSlot.State == 2)
                    {
                        trainingToolSlot.ChangeState(1);
                    }

                    if (trainingToolSlot.TrainingTool == trainingTool)
                    {
                        trainingToolSlot.ChangeState(2);
                    }
                }

                SetSelectedTool(trainingTool);
            }
            else
            {
                foreach (TrainingToolSlotUI trainingToolSlot in trainingToolSlots)
                {
                    if (trainingToolSlot.TrainingTool == trainingTool)
                    {
                        trainingToolSlot.ChangeState(state);
                    }
                }
            }
        }

        public void SetSelectedTool(TrainingToolSO trainingTool)
        {
            trainingToolInfoPanel.Setup(trainingTool);
        }
    }
}
