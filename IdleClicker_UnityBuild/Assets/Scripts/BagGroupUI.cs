using UnityEngine;

namespace IdleClicker
{
    public class BagGroupUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private GameObject bagContent;
        [SerializeField] private TrainingToolSlotUI trainingToolSlotPrefab;

        private GameManager gameManager;

        // Properties
        // Methods

        private void Start()
        {
            gameManager = GameManager.Instance;

            foreach (Transform child in bagContent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (TrainingToolSO trainingTool in gameManager.TrainingTools)
            {
                TrainingToolSlotUI trainingToolSlot = Instantiate(trainingToolSlotPrefab, bagContent.transform);
                trainingToolSlot.Setup(trainingTool);
            }
        }
    }
}
