using IdleClicker.UI;
using System.Collections;
using UnityEngine;

namespace IdleClicker.Training
{
    public class CharacterAnimationTrigger : MonoBehaviour
    {
        // Variables

        [SerializeField] private PopupText popupText;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Transform spawnPopupTextPoint;
        [SerializeField] private float spawnPopupTextCircleRadius;

        private Animator animator;
        private float gainStrength;

        // Methods

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(spawnPopupTextPoint.position, spawnPopupTextCircleRadius);
        }

        public void AnimationTrigger(float gainStrength)
        {
            StartCoroutine(AnimationTriggerCourotine());
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
            popupTextInstance.SetValue(gainStrength);

            Vector2 spawnPosition = spawnPopupTextPoint.position;
            spawnPosition.x += Random.Range(-spawnPopupTextCircleRadius, spawnPopupTextCircleRadius);
            spawnPosition.y += Random.Range(-spawnPopupTextCircleRadius, spawnPopupTextCircleRadius);

            popupText.transform.position = spawnPosition;
        }

        public void GainStrength()
        {
            TrainingManager.Instance.GainStrength();
        }

        public void ResetCanLift()
        {
            TrainingManager.Instance.SetCanLift(true);
        }
    }
}
