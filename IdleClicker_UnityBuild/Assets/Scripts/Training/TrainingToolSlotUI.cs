using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.Training
{
    public class TrainingToolSlotUI : MonoBehaviour
    {
        // Variables

        [Header("Lock content")]
        [SerializeField] private GameObject lockContent;

        [Header("Unlock content")]
        [SerializeField] private GameObject unlockContent;
        [SerializeField] private Image icon;
        [SerializeField] private GameObject selectedIcon;

        private TrainingToolSO trainingTool;
        private TrainingToolManager trainingToolManager;

        // Properties

        public TrainingToolSO TrainingTool => trainingTool;


        // Methods

        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                FindObjectOfType<BagGroupUI>().SetSelectedTool(this.trainingTool);
            });

        }

        private void Start()
        {
            trainingToolManager = FindObjectOfType<TrainingToolManager>();
        }

        private void Update()
        {
            if (this.trainingTool == trainingToolManager.CurrentTrainingTool)
            {
                selectedIcon.SetActive(true);
            }
            else
            {
                selectedIcon.SetActive(false);
            }
        }

        public void Setup(TrainingToolSO trainingTool)
        {
            icon.sprite = trainingTool.Icon;
            this.trainingTool = trainingTool;
            SetUnlock(false);
        }

        public void SetUnlock(bool unlocked)
        {
            Button button = GetComponent<Button>();

            if (unlocked)
            {
                unlockContent.SetActive(true);
                lockContent.SetActive(false);
                button.interactable = true;
            }
            else
            {
                unlockContent.SetActive(false);
                lockContent.SetActive(true);
                button.interactable = false;
            }
        }
    }
}
