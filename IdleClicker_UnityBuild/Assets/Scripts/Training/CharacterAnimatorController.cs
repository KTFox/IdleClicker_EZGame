using System.Collections;
using UnityEngine;

namespace IdleClicker.Training
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        // Variables

        [Header("Popup text setting")]
        [SerializeField] private PopupText popupText;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Transform spawnPopupTextPoint;
        [SerializeField] private float spawnPopupTextCircleRadius;

        [Header("Tool visual")]
        [SerializeField] private SpriteRenderer trainingToolVisual;

        private Animator animator;

        private float gainStrength;

        // Methods

        private void Awake()
        {
            animator = GetComponent<Animator>();

            FindObjectOfType<TrainingToolManager>().OnEquipTrainingTool += TrainingToolManager_OnEquipTrainingTool;
        }

        private void TrainingToolManager_OnEquipTrainingTool(TrainingToolSO trainingTool)
        {
            trainingToolVisual.sprite = trainingTool.ToolVisual;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(spawnPopupTextPoint.position, spawnPopupTextCircleRadius);
        }

        public void AnimationTrigger(float gainStrength, float animatorSpeed)
        {
            StartCoroutine(AnimationTriggerCourotine());
            animator.speed = animatorSpeed;
            this.gainStrength = gainStrength;
        }

        IEnumerator AnimationTriggerCourotine()
        {
            animator.SetBool("act", true);

            yield return new WaitForSeconds(0.09f);

            animator.SetBool("act", false);
        }

        public void SpawnPopupText()
        {
            PopupText popupTextInstance = Instantiate(popupText, canvas.transform);
            popupTextInstance.SetValue((int)gainStrength);

            Vector2 spawnPosition = spawnPopupTextPoint.position;
            spawnPosition.x += Random.Range(-spawnPopupTextCircleRadius, spawnPopupTextCircleRadius);
            spawnPosition.y += Random.Range(-spawnPopupTextCircleRadius, spawnPopupTextCircleRadius);

            popupText.transform.position = spawnPosition;
        }

        public void GainStrength()
        {
            FindObjectOfType<AssetManager>().ChangeStrengthValue(gainStrength);
        }

        public void ResetCanLift()
        {
            FindObjectOfType<TrainingManager>().SetCanLift(true);
        }

        public void ResetAnimatorSpeed()
        {
            animator.speed = 1;
        }
    }
}
