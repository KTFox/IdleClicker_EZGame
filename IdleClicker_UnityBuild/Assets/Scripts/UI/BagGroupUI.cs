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

        private void OnEnable()
        {
            FindObjectOfType<TrainingToolManager>().OnTraingToolManagerUpdate += TrainingToolManager_OnTraingToolManagerUpdate;
        }

        private void TrainingToolManager_OnTraingToolManagerUpdate()
        {
            foreach (Transform child in bagContent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (TrainingToolManager.TrainingToolConfig trainingToolConfig in FindObjectOfType<TrainingToolManager>().TrainingToolConfigs)
            {
                TrainingToolSlotUI trainingToolSlot = Instantiate(trainingToolSlotPrefab, bagContent.transform);
                trainingToolSlot.Setup(trainingToolConfig.TrainingTool);

                if (trainingToolConfig.HasBought)
                {
                    trainingToolSlot.SetUnlock(true);
                }
                else
                {
                    trainingToolSlot.SetUnlock(false);
                }
            }
        }

        public void SetSelectedTool(TrainingToolSO trainingTool)
        {
            trainingToolInfoPanel.Setup(trainingTool);
        }
    }
}
