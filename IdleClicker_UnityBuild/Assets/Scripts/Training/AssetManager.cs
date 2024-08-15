using UnityEngine;

namespace IdleClicker
{
    public class AssetManager : MonoBehaviour
    {
        // Variables

        private float strength;
        private float money;

        // Properties

        public float Strength => strength;
        public float Money => money;


        // Methods

        public void ExchangeStrengthForMoney()
        {
            money += strength;
            strength = 0f;
        }

        public void ChangeMoneyValue(float amount)
        {
            money = Mathf.Max(0, money + amount);
        }

        public void ChangeStrengthValue(float amount)
        {
            strength += amount;
        }
    }
}
