using TMPro;
using UnityEngine;

namespace IdleClicker
{
    public class PopupText : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private float lifeTime;

        private float timer;

        // Methods

        private void OnEnable()
        {
            timer = lifeTime;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Destroy(gameObject);
            }
        }

        public void SetValue(int value)
        {
            valueText.text = $"+{value}";
        }
    }
}
