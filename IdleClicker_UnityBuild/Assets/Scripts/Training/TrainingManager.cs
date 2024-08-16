using UnityEngine;
using UnityEngine.EventSystems;

namespace IdleClicker.Training
{
    public class TrainingManager : MonoBehaviour
    {
        // Variables

        private const float AUTO_LIFTING_SPEED = 4f;

        [SerializeField] private CharacterAnimatorController characterAnimatorController;

        [Header("UI")]
        [SerializeField] private TrainingToolHolderUI trainingToolHolderUI;

        private TrainingToolManager trainingToolManager;

        private bool canLift = true;
        private float autoLiftTimer;

        // Properties


        // Methods

        private void Start()
        {
            trainingToolManager = FindObjectOfType<TrainingToolManager>();
        }

        private void Update()
        {
            HandleAutoLift();
            HandleManualLift();
        }

        private void HandleAutoLift()
        {
            autoLiftTimer -= Time.deltaTime;
            if (autoLiftTimer <= 0)
            {
                autoLiftTimer = AUTO_LIFTING_SPEED;
                trainingToolHolderUI.RunCooldown(trainingToolManager.TrainingAnimationSpeedInRealTime);
                characterAnimatorController.AnimationTrigger(trainingToolManager.EarningPerLift, trainingToolManager.LiftSpeed);
            }
        }

        private void HandleManualLift()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Began)
                {
                    if (canLift)
                    {
                        autoLiftTimer = AUTO_LIFTING_SPEED;
                        canLift = false;
                        trainingToolHolderUI.RunCooldown(trainingToolManager.TrainingAnimationSpeedInRealTime);
                        characterAnimatorController.AnimationTrigger(trainingToolManager.EarningPerLift, trainingToolManager.LiftSpeed);
                    }
                }
            }
        }

        public void SetCanLift(bool value)
        {
            canLift = value;
        }
    }
}
