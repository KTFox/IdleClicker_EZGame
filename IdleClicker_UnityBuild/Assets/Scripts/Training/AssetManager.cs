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

        private void Awake()
        {
            PersistenceData persistenceData = Resources.Load<PersistenceData>("PersistenceData");
            if (persistenceData == null)
            {
                Debug.LogError("Persistence data is not found");
            }

            strength = persistenceData.Strength;
            money = persistenceData.Money;
        }

        private void OnDestroy()
        {
            PersistenceData persistenceData = Resources.Load<PersistenceData>("PersistenceData");
            if (persistenceData == null)
            {
                Debug.LogError("Persistence data is not found");
            }

            persistenceData.Strength = strength;
            persistenceData.Money = money;
        }

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
