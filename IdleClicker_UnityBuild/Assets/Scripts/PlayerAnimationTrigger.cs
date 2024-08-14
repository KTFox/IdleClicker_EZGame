using System.Collections;
using UnityEngine;

namespace IdleClicker
{
    public class PlayerAnimationTrigger : MonoBehaviour
    {
        // Variables

        private Animator animator;

        // Methods

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void AnimationTrigger()
        {
            StartCoroutine(AnimationTriggerCourotine());
        }

        IEnumerator AnimationTriggerCourotine()
        {
            animator.SetBool("act", true);

            yield return new WaitForSeconds(0.09f);

            animator.SetBool("act", false);
        }
    }
}
