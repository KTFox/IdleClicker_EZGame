using UnityEngine;

namespace IdleClicker
{
    public class BagGroupUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private GameObject bagContent;
        [SerializeField] private TrainingToolSlotUI trainingToolSlotPrefab;


        // Methods

        private void Awake()
        {
            foreach (Transform child in bagContent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (TrainingToolSO trainingTool in FindObjectOfType<GameManager>().TrainingTools)
            {
                TrainingToolSlotUI trainingToolSlot = Instantiate(trainingToolSlotPrefab, bagContent.transform);
                trainingToolSlot.Setup(trainingTool);
            }
        }

        public void UpdateTrainingToolSlotState(TrainingToolSO trainingToolSO, int state)
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

                    if (trainingToolSlot.TrainingTool == trainingToolSO)
                    {
                        trainingToolSlot.ChangeState(state);
                    }
                }
            }
            else
            {
                foreach (TrainingToolSlotUI trainingToolSlot in trainingToolSlots)
                {
                    if (trainingToolSlot.TrainingTool == trainingToolSO)
                    {
                        trainingToolSlot.ChangeState(state);
                    }
                }
            }
        }
    }
}
